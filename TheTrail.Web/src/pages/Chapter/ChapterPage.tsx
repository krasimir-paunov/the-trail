import { useEffect, useState, useRef } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { motion, AnimatePresence } from 'framer-motion'
import { chaptersApi } from '../../api/chaptersApi.ts'
import { useAuth } from '../../context/AuthContext.tsx'
import type { ChapterDto, QuizQuestion } from '../../types/index.ts'

// ── Content block types ────────────────────────────────────────────────
interface ParagraphBlock { type: 'paragraph'; text: string }
interface FactBlock { type: 'fact'; title: string; text: string }
interface ImageBlock { type: 'image'; url: string; caption: string }
interface TimelineBlock { type: 'timeline'; date: string; evnt: string }
interface QuoteBlock { type: 'quote'; text: string; source: string }
type ContentBlock = ParagraphBlock | FactBlock | ImageBlock | TimelineBlock | QuoteBlock

export default function ChapterPage() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const { isAuthenticated } = useAuth()
  const bottomRef = useRef<HTMLDivElement>(null)
  const [chapter, setChapter] = useState<ChapterDto | null>(null)
  const [blocks, setBlocks] = useState<ContentBlock[]>([])
  const [loading, setLoading] = useState(true)
  const [scrollProgress, setScrollProgress] = useState(0)
  const [scrollCompleted, setScrollCompleted] = useState(false)
  const [showQuiz, setShowQuiz] = useState(false)
  const [quizQuestions, setQuizQuestions] = useState<QuizQuestion[]>([])
  const [currentQuestion, setCurrentQuestion] = useState(0)
  const [selectedAnswer, setSelectedAnswer] = useState<string | null>(null)
  const [answeredCorrectly, setAnsweredCorrectly] = useState<boolean | null>(null)
  const [answers, setAnswers] = useState<Record<number, boolean>>({})
  const [score, setScore] = useState(0)
  const [quizComplete, setQuizComplete] = useState(false)
  const [quizPassed, setQuizPassed] = useState(false)
  const [showReward, setShowReward] = useState(false)

  useEffect(() => {
    if (!id) return
    const chapterId = parseInt(id)

    Promise.all([
      chaptersApi.getById(chapterId),
      chaptersApi.getContent(chapterId),
      chaptersApi.getQuiz(chapterId),
    ]).then(([chapterData, contentData, quizData]) => {
      setChapter(chapterData)
      setScrollCompleted(chapterData.scrollCompleted)
      setQuizPassed(chapterData.quizPassed)
      try {
        const raw = typeof contentData === 'string' ? contentData : JSON.stringify(contentData)
        const parsed = JSON.parse(raw)
        const actualBlocks = typeof parsed === 'string' ? JSON.parse(parsed) : parsed
        setBlocks(Array.isArray(actualBlocks) ? actualBlocks : [])
      } catch {
        setBlocks([])
      }
      setQuizQuestions(quizData ?? [])
      setLoading(false)
    }).catch(() => navigate('/'))
  }, [id, navigate])

  // ── Scroll progress tracking ───────────────────────────────────────
  useEffect(() => {
    const handleScroll = () => {
      const scrollTop = window.scrollY
      const docHeight = document.documentElement.scrollHeight - window.innerHeight
      const progress = docHeight > 0 ? (scrollTop / docHeight) * 100 : 0
      setScrollProgress(Math.min(progress, 100))

      if (progress >= 95 && !scrollCompleted) {
        setScrollCompleted(true)
        if (isAuthenticated && id) {
          chaptersApi.completeScroll(parseInt(id)).catch(() => {})
        }
      }
    }

    window.addEventListener('scroll', handleScroll)
    return () => window.removeEventListener('scroll', handleScroll)
  }, [scrollCompleted, isAuthenticated, id])

  // ── Quiz logic ─────────────────────────────────────────────────────
  const handleAnswer = (option: string) => {
    if (selectedAnswer) return
    setSelectedAnswer(option)

    const token = localStorage.getItem('token')

    fetch(`https://localhost:7134/api/chapters/${id}/quiz/check`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...(token ? { 'Authorization': `Bearer ${token}` } : {})
      },
      body: JSON.stringify({
        questionId: quizQuestions[currentQuestion].id,
        answer: option
      })
    })
      .then(r => r.json())
      .then((correct: boolean) => {
        setAnsweredCorrectly(correct)
        setAnswers(prev => ({ ...prev, [currentQuestion]: correct }))
        if (correct) setScore(s => s + 1)

        setTimeout(() => {
          if (currentQuestion < quizQuestions.length - 1) {
            setCurrentQuestion(q => q + 1)
            setSelectedAnswer(null)
            setAnsweredCorrectly(null)
          } else {
            const finalScore = correct ? score + 1 : score
            const passPercent = Math.round((finalScore / quizQuestions.length) * 100)
            const isPassed = passPercent >= 60
            setQuizComplete(true)
            setQuizPassed(isPassed)
            if (isPassed && isAuthenticated && id) {
              chaptersApi.saveQuizResult(parseInt(id), true).catch(() => {})
            }
            if (isPassed) {
              setTimeout(() => setShowReward(true), 800)
            }
          }
        }, 1500)
      })
      .catch(() => {
        setAnsweredCorrectly(false)
        setAnswers(prev => ({ ...prev, [currentQuestion]: false }))
        setTimeout(() => {
          setSelectedAnswer(null)
          setAnsweredCorrectly(null)
        }, 1500)
      })
  }

  const getOptionClass = (option: string) => {
    if (!selectedAnswer) {
      return 'border-stone-700 text-stone-300 hover:border-amber-200/50 hover:text-amber-200 cursor-pointer'
    }
    if (answeredCorrectly && option === selectedAnswer) {
      return 'border-green-500 text-green-400 bg-green-500/10'
    }
    if (!answeredCorrectly && option === selectedAnswer) {
      return 'border-red-500 text-red-400 bg-red-500/10'
    }
    return 'border-stone-800 text-stone-600'
  }

  const resetQuiz = () => {
    setCurrentQuestion(0)
    setSelectedAnswer(null)
    setAnsweredCorrectly(null)
    setAnswers({})
    setScore(0)
    setQuizComplete(false)
  }

  if (loading || !chapter) {
    return (
      <div className="min-h-screen bg-[#1e1a12] flex items-center justify-center">
        <motion.div
          animate={{ opacity: [0.3, 1, 0.3] }}
          transition={{ duration: 2, repeat: Infinity }}
          className="text-amber-200/50 text-xl tracking-widest uppercase"
        >
          Opening the scroll...
        </motion.div>
      </div>
    )
  }

  const passed = quizPassed || Math.round((score / quizQuestions.length) * 100) >= 60

  return (
    <div className="min-h-screen bg-[#1e1a12]">
      {/* Reading progress bar */}
      <div className="fixed top-0 left-0 right-0 h-0.5 bg-stone-900 z-40">
        <motion.div
          className="h-full bg-amber-400/70"
          style={{ width: `${scrollProgress}%` }}
          transition={{ duration: 0.1 }}
        />
      </div>

      {/* Chapter Hero */}
      <div className="relative h-[30vh] flex flex-col items-center justify-end pb-10 px-4">
        <div className="absolute inset-0 bg-linear-to-b from-stone-900 to-[#1e1a12]" />
        <div className="absolute inset-0 bg-linear-to-t from-[#1e1a12] via-transparent to-transparent" />

        <motion.button
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          onClick={() => navigate(-1)}
          className="absolute top-8 left-8 text-stone-500 hover:text-amber-200 transition-colors text-sm tracking-widest uppercase cursor-pointer"
        >
          ← Back
        </motion.button>

        <div className="relative z-10 text-center max-w-3xl">
          <motion.p
            initial={{ opacity: 0, y: 10 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.2 }}
            className="text-amber-300/50 text-xs tracking-[0.4em] uppercase mb-3"
          >
            {chapter.estimatedMinutes} min read
          </motion.p>
          <motion.h1
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.3 }}
            className="text-4xl md:text-6xl font-bold text-amber-50 mb-4"
          >
            {chapter.title}
          </motion.h1>
          <motion.p
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.5 }}
            className="text-stone-400 text-lg"
          >
            {chapter.subtitle}
          </motion.p>
        </div>
      </div>

      {/* Two column layout */}
      <div className="flex gap-0 py-20 max-w-6xl mx-auto px-4">

        {/* Sticky left sidebar */}
        <div className="hidden lg:flex flex-col w-52 shrink-0 pr-8">
          <div className="sticky top-24 flex flex-col gap-6">
            <div>
              <p className="text-amber-200/30 text-xs tracking-[0.3em] uppercase mb-1">
                Chapter
              </p>
              <p className="text-amber-200/60 text-sm font-bold leading-snug">
                {chapter.title}
              </p>
            </div>

            <div>
              <p className="text-amber-200/30 text-xs tracking-[0.3em] uppercase mb-2">
                Reading
              </p>
              <p className="text-amber-200/60 text-3xl font-bold mb-3">
                {Math.round(scrollProgress)}%
              </p>
              <div className="w-px h-32 bg-stone-800 relative ml-1">
                <motion.div
                  className="w-px bg-amber-400/60 absolute top-0 left-0"
                  style={{ height: `${scrollProgress}%` }}
                  transition={{ duration: 0.1 }}
                />
              </div>
            </div>

            <div>
              <p className="text-amber-200/30 text-xs tracking-[0.3em] uppercase mb-3">
                Progress
              </p>
              <div className="flex flex-col gap-3">
                <div className="flex items-center gap-3">
                  <div className={`w-2 h-2 rounded-full border transition-colors duration-500 ${
                    scrollCompleted
                      ? 'bg-amber-200 border-amber-200'
                      : 'border-stone-600'
                  }`} />
                  <p className="text-stone-500 text-xs tracking-wide">Read</p>
                </div>
                <div className="flex items-center gap-3">
                  <div className={`w-2 h-2 rounded-full border transition-colors duration-500 ${
                    quizPassed
                      ? 'bg-yellow-400 border-yellow-400'
                      : 'border-stone-600'
                  }`} />
                  <p className="text-stone-500 text-xs tracking-wide">Quiz</p>
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* Main content */}
        <div className="flex-1 border-l border-amber-200/10 pl-8">
          {blocks.length === 0 ? (
            <p className="text-stone-500 text-center py-16">Content coming soon.</p>
          ) : (
            <div className="flex flex-col gap-12">
              {blocks.map((block, i) => (
                <>
                  {i > 0 && i % 3 === 0 && (
                    <motion.div
                      key={`divider-${i}`}
                      initial={{ opacity: 0 }}
                      whileInView={{ opacity: 1 }}
                      viewport={{ once: true }}
                      className="flex items-center justify-center gap-4 py-2"
                    >
                      <div className="h-px flex-1 bg-amber-200/10" />
                      <span className="text-amber-200/20 text-xs">✦</span>
                      <div className="h-px flex-1 bg-amber-200/10" />
                    </motion.div>
                  )}
                  <motion.div
                    key={i}
                    initial={{ opacity: 0, y: 20 }}
                    whileInView={{ opacity: 1, y: 0 }}
                    viewport={{ once: true }}
                    transition={{ duration: 0.7 }}
                  >
                    {block.type === 'paragraph' && (
                      <p className={`text-stone-200 text-xl leading-[1.9] ${i === 0 ? 'drop-cap' : ''}`}>
                        {block.text}
                      </p>
                    )}

                    {block.type === 'fact' && (
                      <div className="bg-amber-950/30 border border-amber-200/20 px-8 py-6">
                        <p className="text-amber-300/80 text-xs tracking-[0.3em] uppercase mb-3">
                          {block.title}
                        </p>
                        <p className="text-amber-100 text-lg leading-relaxed">
                          {block.text}
                        </p>
                      </div>
                    )}

                    {block.type === 'image' && (
                      <div>
                        <img
                          src={block.url}
                          alt={block.caption}
                          className="w-full object-cover mb-4"
                        />
                        <p className="text-stone-400 text-base text-center italic mt-3">
                          {block.caption}
                        </p>
                      </div>
                    )}

                    {block.type === 'timeline' && (
                      <div className="border-l-2 border-amber-200/20 pl-8 py-4">
                        <p className="text-amber-300/70 text-sm tracking-widest uppercase font-bold mb-2">
                          {block.date}
                        </p>
                        <p className="text-stone-200 text-lg leading-relaxed">
                          {block.evnt}
                        </p>
                      </div>
                    )}

                    {block.type === 'quote' && (
                      <div className="border-t border-b border-amber-200/20 py-10 my-4 text-center">
                        <p className="text-amber-100/90 text-2xl md:text-3xl italic leading-relaxed mb-4">
                          "{block.text}"
                        </p>
                        <p className="text-stone-500 text-xs tracking-[0.3em] uppercase">
                          — {block.source}
                        </p>
                      </div>
                    )}
                  </motion.div>
                </>
              ))}
            </div>
          )}

          {/* Scroll completion */}
          <div ref={bottomRef} className="mt-24">
            <AnimatePresence>
              {scrollCompleted && !showQuiz && !quizPassed && (
                <motion.div
                  initial={{ opacity: 0, y: 30 }}
                  animate={{ opacity: 1, y: 0 }}
                  exit={{ opacity: 0 }}
                  transition={{ duration: 0.8 }}
                  className="text-center border-t border-amber-200/10 pt-16"
                >
                  <motion.p
                    animate={{ opacity: [0.5, 1, 0.5] }}
                    transition={{ duration: 3, repeat: Infinity }}
                    className="text-amber-200/50 text-xs tracking-[0.4em] uppercase mb-4"
                  >
                    You have walked this trail
                  </motion.p>
                  <p className="text-stone-400 text-sm mb-8">
                    {chapter.hasQuiz
                      ? 'Prove your knowledge to earn your reward.'
                      : 'Chapter complete.'}
                  </p>
                  {chapter.hasQuiz && quizQuestions.length > 0 && (
                    <motion.button
                      whileHover={{ scale: 1.05 }}
                      whileTap={{ scale: 0.95 }}
                      onClick={() => setShowQuiz(true)}
                      className="px-8 py-4 border border-amber-200/30 text-amber-200/80 text-sm tracking-widest uppercase hover:border-amber-200/60 hover:text-amber-50 transition-all duration-300 cursor-pointer"
                    >
                      Test Your Knowledge →
                    </motion.button>
                  )}
                </motion.div>
              )}

              {quizPassed && (
                <motion.div
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  className="text-center border-t border-amber-200/10 pt-16"
                >
                  <p className="text-yellow-400/70 text-xs tracking-[0.4em] uppercase mb-2">
                    ★ Chapter Complete
                  </p>
                  <p className="text-stone-500 text-sm">
                    You have mastered this chapter.
                  </p>
                </motion.div>
              )}
            </AnimatePresence>
          </div>
        </div>
      </div>

      {/* Quiz overlay */}
      <AnimatePresence>
        {showQuiz && !quizComplete && (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            className="fixed inset-0 z-50 bg-stone-950/95 backdrop-blur-sm flex items-center justify-center px-4"
          >
            <motion.button
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ delay: 0.3 }}
              onClick={() => {
                setShowQuiz(false)
                resetQuiz()
              }}
              className="absolute top-6 right-6 text-stone-500 hover:text-amber-200 transition-colors text-sm tracking-widest uppercase cursor-pointer"
            >
              ✕ Close
            </motion.button>

            <div className="w-full max-w-2xl">
              <div className="flex items-center justify-between mb-8">
                <p className="text-stone-500 text-xs tracking-widest uppercase">
                  Question {currentQuestion + 1} of {quizQuestions.length}
                </p>
                <div className="flex gap-2">
                  {quizQuestions.map((_, i) => (
                    <div
                      key={i}
                      className={`w-2 h-2 rounded-full transition-all duration-500 ${
                        i < currentQuestion
                          ? answers[i] === true
                            ? 'bg-green-400'
                            : answers[i] === false
                            ? 'bg-red-400'
                            : 'bg-stone-600'
                          : i === currentQuestion
                          ? 'bg-amber-200/60 ring-1 ring-amber-200/30'
                          : 'bg-stone-700'
                      }`}
                    />
                  ))}
                </div>
              </div>

              <AnimatePresence mode="wait">
                <motion.div
                  key={currentQuestion}
                  initial={{ opacity: 0, x: 30 }}
                  animate={{ opacity: 1, x: 0 }}
                  exit={{ opacity: 0, x: -30 }}
                  transition={{ duration: 0.3 }}
                >
                  <h2 className="text-2xl md:text-3xl font-bold text-amber-50 mb-10 leading-relaxed">
                    {quizQuestions[currentQuestion]?.text}
                  </h2>

                  <div className="grid grid-cols-1 gap-3">
                    {(['A', 'B', 'C', 'D'] as const).map((option) => {
                      const optionKey = `option${option}` as keyof QuizQuestion
                      return (
                        <motion.button
                          key={option}
                          whileHover={!selectedAnswer ? { scale: 1.01 } : {}}
                          whileTap={!selectedAnswer ? { scale: 0.99 } : {}}
                          onClick={() => handleAnswer(option)}
                          className={`flex items-center gap-4 p-4 border text-left transition-all duration-300 ${getOptionClass(option)}`}
                        >
                          <span className="text-xs tracking-widest opacity-50 shrink-0">
                            {option}
                          </span>
                          <span className="text-base">
                            {quizQuestions[currentQuestion]?.[optionKey] as string}
                          </span>
                        </motion.button>
                      )
                    })}
                  </div>

                  {answeredCorrectly !== null && (
                    <motion.p
                      initial={{ opacity: 0 }}
                      animate={{ opacity: 1 }}
                      className={`text-center mt-6 text-sm tracking-widest uppercase ${
                        answeredCorrectly ? 'text-green-400' : 'text-red-400'
                      }`}
                    >
                      {answeredCorrectly ? 'Correct' : 'Incorrect'}
                    </motion.p>
                  )}
                </motion.div>
              </AnimatePresence>
            </div>
          </motion.div>
        )}
      </AnimatePresence>

      {/* Quiz results */}
      <AnimatePresence>
        {quizComplete && !showReward && (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            className="fixed inset-0 z-50 bg-stone-950/95 backdrop-blur-sm flex items-center justify-center px-4"
          >
            <motion.div
              initial={{ scale: 0.9, opacity: 0 }}
              animate={{ scale: 1, opacity: 1 }}
              transition={{ delay: 0.2, duration: 0.5 }}
              className="text-center max-w-md"
            >
              <p className="text-stone-500 text-xs tracking-[0.4em] uppercase mb-6">
                Results
              </p>
              <p className={`text-7xl font-bold mb-4 ${passed ? 'text-amber-200' : 'text-stone-400'}`}>
                {score}/{quizQuestions.length}
              </p>
              <p className={`text-lg mb-2 ${passed ? 'text-amber-100' : 'text-stone-400'}`}>
                {passed ? 'Excellent' : 'Not quite'}
              </p>
              <p className="text-stone-500 text-sm mb-12">
                {passed
                  ? 'You have proven your knowledge of this era.'
                  : 'The trail is long. Study and try again.'}
              </p>

              <div className="flex gap-4 justify-center">
                {!passed && (
                  <motion.button
                    whileHover={{ scale: 1.05 }}
                    onClick={resetQuiz}
                    className="px-6 py-3 border border-stone-700 text-stone-400 text-sm tracking-widest uppercase hover:border-stone-500 hover:text-stone-200 transition-all cursor-pointer"
                  >
                    Try Again
                  </motion.button>
                )}
                <motion.button
                  whileHover={{ scale: 1.05 }}
                  onClick={() => navigate(-1)}
                  className="px-6 py-3 border border-amber-200/30 text-amber-200/70 text-sm tracking-widest uppercase hover:border-amber-200/60 hover:text-amber-50 transition-all cursor-pointer"
                >
                  Return to Era
                </motion.button>
              </div>
            </motion.div>
          </motion.div>
        )}
      </AnimatePresence>

      {/* Collectible reward */}
      <AnimatePresence>
        {showReward && (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            className="fixed inset-0 z-50 flex items-center justify-center px-4 backdrop-blur-xl"
            style={{
              background: 'radial-gradient(ellipse at center, rgba(180,120,30,0.15) 0%, rgba(0,0,0,0.96) 60%)'
            }}
          >
            <div className="text-center max-w-sm">
              <motion.p
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                transition={{ delay: 0.3 }}
                className="text-amber-200/50 text-xs tracking-[0.5em] uppercase mb-8"
              >
                Reward Unlocked
              </motion.p>

              <motion.div
                initial={{ scale: 0, opacity: 0, y: 40 }}
                animate={{ scale: 1, opacity: 1, y: 0 }}
                transition={{ delay: 0.5, duration: 0.8, ease: [0.32, 0.72, 0, 1] }}
                className="relative inline-block mb-8"
              >
                <motion.div
                  animate={{ opacity: [0.3, 0.7, 0.3], scale: [1, 1.15, 1] }}
                  transition={{ duration: 2.5, repeat: Infinity }}
                  className="absolute inset-0 rounded-full bg-amber-400/20 blur-2xl"
                />
                <div className="relative w-64 h-64 rounded-full border-2 border-amber-200/40 overflow-hidden">
                  <img
                    src="/images/collectibles/trex.jpg"
                    alt="T-Rex Collectible"
                    className="w-full h-full object-cover"
                  />
                  <div className="absolute inset-0 rounded-full border-4 border-amber-400/20" />
                </div>
              </motion.div>

              <motion.div
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ delay: 1, duration: 0.6 }}
                className="bg-black/60 backdrop-blur-sm px-8 py-6 rounded-2xl border border-amber-200/10"
              >
                <p className="text-amber-50 text-2xl font-bold mb-2">T-Rex</p>
                <p className="text-stone-400/60 text-xs tracking-[0.3em] uppercase mb-4">
                    Common Collectible
                </p>
                <p className="text-stone-300 text-sm mb-8 leading-relaxed">
                  The apex predator of the prehistoric era. Earned by those who walked the trail and proved their knowledge.
                </p>

                <motion.button
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  transition={{ delay: 1.5 }}
                  whileHover={{ scale: 1.05 }}
                  onClick={() => navigate(-1)}
                  className="px-8 py-3 border border-amber-200/40 text-amber-200/80 text-sm tracking-widest uppercase hover:border-amber-200/70 hover:text-amber-50 transition-all cursor-pointer"
                >
                  Continue The Trail →
                </motion.button>
              </motion.div>
            </div>
          </motion.div>
        )}
      </AnimatePresence>
    </div>
  )
}