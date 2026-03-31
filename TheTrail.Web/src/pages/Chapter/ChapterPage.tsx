import { useEffect, useState, useRef } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { motion, AnimatePresence } from 'framer-motion'
import { chaptersApi } from '../../api/chaptersApi.ts'
import { useAuth } from '../../context/AuthContext.tsx'
import type { ChapterDto, QuizQuestion } from '../../types/index.ts'
import EraGrandmasterModal from '../../components/EraGrandmasterModal.tsx'

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
  const [perfectScore, setPerfectScore] = useState(false)
  const [showReward, setShowReward] = useState(false)
  const [earnedRare, setEarnedRare] = useState(false)
  const [hasEarnedRare, setHasEarnedRare] = useState(false)

  // ── Grandmaster state ─────────────────────────────────────────────
  const [showGrandmaster, setShowGrandmaster] = useState(false)
  const [grandmasterData, setGrandmasterData] = useState<{
    eraName: string
    legendaryName: string
    legendaryDescription: string
    legendaryImageUrl: string | null
  } | null>(null)

  // ── Load chapter data ─────────────────────────────────────────────
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

  // ── Check localStorage for rare earned status ─────────────────────
  useEffect(() => {
    if (!id) return
    if (localStorage.getItem(`rare_earned_${id}`) === 'true') {
      setHasEarnedRare(true)
    }
  }, [id])

  // ── Scroll progress ───────────────────────────────────────────────
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

  // ── Quiz logic ────────────────────────────────────────────────────
  const handleAnswer = (option: string) => {
    if (selectedAnswer) return
    setSelectedAnswer(option)

    const headers: Record<string, string> = { 'Content-Type': 'application/json' }
    const token = localStorage.getItem('token')
    if (token) headers['Authorization'] = `Bearer ${token}`

    fetch(`https://localhost:7134/api/chapters/${id}/quiz/check`, {
      method: 'POST',
      headers,
      body: JSON.stringify({
        questionId: quizQuestions[currentQuestion].id,
        answer: option
      })
    })
      .then(r => {
        if (!r.ok) throw new Error(`HTTP ${r.status}`)
        return r.json()
      })
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
            setScore(finalScore => {
              const passPercent = Math.round((finalScore / quizQuestions.length) * 100)
              const isPassed = passPercent >= 60
              const isPerfect = finalScore === quizQuestions.length

              setPerfectScore(isPerfect)

              if (isPerfect) {
                setEarnedRare(true)
                setHasEarnedRare(true)
                if (id) localStorage.setItem(`rare_earned_${id}`, 'true')
              }

              setTimeout(() => {
                setQuizComplete(true)
                setQuizPassed(isPassed)

                if (isPassed) {
                  if (isAuthenticated && id) {
                    chaptersApi.saveQuizResult(parseInt(id), isPassed, isPerfect)
                      .then((result) => {
                        if (result?.legendaryAwarded) {
                          setGrandmasterData({
                            eraName: result.eraName ?? '',
                            legendaryName: result.legendaryName ?? 'Era Grandmaster',
                            legendaryDescription: result.legendaryDescription ?? '',
                            legendaryImageUrl: result.legendaryImageUrl ?? null,
                          })
                        }
                      })
                      .catch(() => {})
                      .finally(() => {
                        setTimeout(() => setShowReward(true), 1200)
                      })
                  } else {
                    setTimeout(() => setShowReward(true), 1200)
                  }
                }
              }, 100)

              return finalScore
            })
          }
        }, 1500)
      })
      .catch((err) => {
        console.error('Quiz check error:', err)
        setAnsweredCorrectly(false)
        setAnswers(prev => ({ ...prev, [currentQuestion]: false }))

        setTimeout(() => {
          if (currentQuestion < quizQuestions.length - 1) {
            setCurrentQuestion(q => q + 1)
            setSelectedAnswer(null)
            setAnsweredCorrectly(null)
          } else {
            setScore(finalScore => {
              const passPercent = Math.round((finalScore / quizQuestions.length) * 100)
              const isPassed = passPercent >= 60
              const isPerfect = finalScore === quizQuestions.length
              setQuizComplete(true)
              setQuizPassed(isPassed)
              setPerfectScore(isPerfect)
              return finalScore
            })
          }
        }, 1500)
      })
  }

  // ── resetQuiz — never touches quizPassed (server state) ──────────
  const resetQuiz = () => {
    setCurrentQuestion(0)
    setSelectedAnswer(null)
    setAnsweredCorrectly(null)
    setAnswers({})
    setScore(0)
    setQuizComplete(false)
    setPerfectScore(false)
    setEarnedRare(false)
    setShowReward(false)
    // quizPassed intentionally NOT reset — it reflects server state
  }

  const getOptionStyle = (option: string) => {
    if (!selectedAnswer) {
      return {
        border: '1px solid #c4a47a',
        color: 'var(--ink-medium)',
        background: 'var(--parchment-light)',
      }
    }
    if (answeredCorrectly && option === selectedAnswer) {
      return {
        border: '1px solid #4ade80',
        color: '#4ade80',
        background: 'rgba(74,222,128,0.08)',
      }
    }
    if (!answeredCorrectly && option === selectedAnswer) {
      return {
        border: '1px solid #f87171',
        color: '#f87171',
        background: 'rgba(248,113,113,0.08)',
      }
    }
    return {
      border: '1px solid #c4a47a40',
      color: 'var(--ink-muted)',
      background: 'var(--parchment-mid)',
    }
  }

  if (loading || !chapter) {
    return (
      <div className="min-h-screen flex items-center justify-center"
        style={{ background: 'var(--hero-dark)' }}>
        <motion.div
          animate={{ opacity: [0.3, 1, 0.3] }}
          transition={{ duration: 2, repeat: Infinity }}
          className="text-xl tracking-widest uppercase"
          style={{ color: 'var(--accent-amber)' }}
        >
          Opening the scroll...
        </motion.div>
      </div>
    )
  }

  const passed = quizPassed || Math.round((score / quizQuestions.length) * 100) >= 60

  return (
    <div className="min-h-screen" style={{ background: 'var(--hero-dark)' }}>

      {/* Reading progress bar */}
      <div className="fixed top-0 left-0 right-0 h-0.5 z-40"
        style={{ background: 'var(--parchment-border)' }}>
        <motion.div
          className="h-full"
          style={{ width: `${scrollProgress}%`, background: 'var(--accent-amber)' }}
          transition={{ duration: 0.1 }}
        />
      </div>

      {/* Chapter Hero */}
      <div className="relative h-[35vh] flex flex-col items-center justify-end pb-12 px-4">
        <div className="absolute inset-0"
          style={{ background: 'linear-gradient(to bottom, #1a0f05, var(--hero-dark))' }} />
        <div className="absolute inset-0"
          style={{ background: 'linear-gradient(to top, var(--hero-dark), transparent)' }} />

        <motion.button
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          onClick={() => navigate(-1)}
          className="absolute top-8 left-8 text-sm tracking-widest uppercase cursor-pointer transition-colors duration-300 hover:opacity-100"
          style={{ color: 'var(--ink-muted)', opacity: 0.7 }}
        >
          ← Back
        </motion.button>

        <div className="relative z-10 text-center max-w-4xl">
          <motion.p
            initial={{ opacity: 0, y: 10 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.2 }}
            className="text-sm tracking-[0.4em] uppercase mb-4"
            style={{ color: 'var(--accent-amber)', opacity: 0.7 }}
          >
            {chapter.estimatedMinutes} min read
          </motion.p>
          <motion.h1
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.3 }}
            className="text-5xl md:text-7xl font-bold mb-4"
            style={{ color: 'var(--parchment-light)', fontFamily: "'Cinzel', serif" }}
          >
            {chapter.title}
          </motion.h1>
          <motion.p
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.5 }}
            className="text-xl"
            style={{ color: 'var(--parchment-dark)' }}
          >
            {chapter.subtitle}
          </motion.p>
        </div>
      </div>

      {/* Parchment content area */}
      <div className="parchment">
        <div className="flex gap-0 max-w-6xl mx-auto px-4 py-20">

          {/* Sticky sidebar */}
          <div className="hidden lg:flex flex-col w-56 shrink-0 pr-10">
            <div className="sticky top-24 flex flex-col gap-8">
              <div>
                <p className="text-xs tracking-[0.3em] uppercase mb-2"
                  style={{ color: 'var(--ink-muted)' }}>Chapter</p>
                <p className="text-base font-bold leading-snug"
                  style={{ color: 'var(--ink-dark)', fontFamily: "'Cinzel', serif" }}>
                  {chapter.title}
                </p>
              </div>
              <div>
                <p className="text-xs tracking-[0.3em] uppercase mb-2"
                  style={{ color: 'var(--ink-muted)' }}>Reading</p>
                <p className="text-4xl font-bold mb-4"
                  style={{ color: 'var(--ink-dark)', fontFamily: "'Cinzel', serif" }}>
                  {Math.round(scrollProgress)}%
                </p>
                <div className="w-px h-36 relative ml-1"
                  style={{ background: 'var(--parchment-border)' }}>
                  <motion.div
                    className="w-px absolute top-0 left-0"
                    style={{ height: `${scrollProgress}%`, background: 'var(--accent-amber)' }}
                    transition={{ duration: 0.1 }}
                  />
                </div>
              </div>
              <div>
                <p className="text-xs tracking-[0.3em] uppercase mb-4"
                  style={{ color: 'var(--ink-muted)' }}>Progress</p>
                <div className="flex flex-col gap-4">
                  <div className="flex items-center gap-3">
                    <div className="w-3 h-3 rounded-full border-2 transition-colors duration-500"
                      style={{
                        background: scrollCompleted ? 'var(--accent-amber)' : 'transparent',
                        borderColor: scrollCompleted ? 'var(--accent-amber)' : 'var(--parchment-border)'
                      }} />
                    <p className="text-sm" style={{ color: 'var(--ink-light)' }}>Read</p>
                  </div>
                  <div className="flex items-center gap-3">
                    <div className="w-3 h-3 rounded-full border-2 transition-colors duration-500"
                      style={{
                        background: quizPassed ? '#eab308' : 'transparent',
                        borderColor: quizPassed ? '#eab308' : 'var(--parchment-border)'
                      }} />
                    <p className="text-sm" style={{ color: 'var(--ink-light)' }}>Quiz</p>
                  </div>
                </div>
              </div>
            </div>
          </div>

          {/* Main content */}
          <div className="flex-1 pl-10"
            style={{ borderLeft: '1px solid var(--parchment-border)' }}>

            {blocks.length === 0 ? (
              <p className="text-center py-16 text-lg" style={{ color: 'var(--ink-muted)' }}>
                Content coming soon.
              </p>
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
                        className="flex items-center justify-center gap-6 py-2"
                      >
                        <div className="h-px flex-1" style={{ background: 'var(--parchment-border)' }} />
                        <span className="text-sm" style={{ color: 'var(--accent-amber-dim)' }}>✦</span>
                        <div className="h-px flex-1" style={{ background: 'var(--parchment-border)' }} />
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
                        <p className={`text-xl leading-[1.9] ${i === 0 ? 'drop-cap' : ''}`}
                          style={{ color: 'var(--ink-dark)', fontFamily: "'EB Garamond', serif" }}>
                          {block.text}
                        </p>
                      )}
                      {block.type === 'fact' && (
                        <div className="px-8 py-6"
                          style={{ background: 'var(--parchment-mid)', borderLeft: '3px solid var(--accent-amber)' }}>
                          <p className="text-xs tracking-[0.3em] uppercase mb-3"
                            style={{ color: 'var(--accent-amber)' }}>{block.title}</p>
                          <p className="text-xl leading-relaxed"
                            style={{ color: 'var(--ink-medium)', fontFamily: "'EB Garamond', serif" }}>
                            {block.text}
                          </p>
                        </div>
                      )}
                      {block.type === 'image' && (
                        <div>
                          <img src={block.url} alt={block.caption} className="w-full object-cover mb-4" />
                          <p className="text-base text-center italic mt-3"
                            style={{ color: 'var(--ink-muted)', fontFamily: "'EB Garamond', serif" }}>
                            {block.caption}
                          </p>
                        </div>
                      )}
                      {block.type === 'timeline' && (
                        <div className="pl-8 py-4"
                          style={{ borderLeft: '2px solid var(--parchment-border)' }}>
                          <p className="text-sm tracking-widest uppercase font-bold mb-3"
                            style={{ color: 'var(--accent-amber)' }}>{block.date}</p>
                          <p className="text-xl leading-relaxed"
                            style={{ color: 'var(--ink-dark)', fontFamily: "'EB Garamond', serif" }}>
                            {block.evnt}
                          </p>
                        </div>
                      )}
                      {block.type === 'quote' && (
                        <div className="py-10 my-4 text-center"
                          style={{
                            borderTop: '1px solid var(--parchment-border)',
                            borderBottom: '1px solid var(--parchment-border)'
                          }}>
                          <p className="text-2xl md:text-3xl italic leading-relaxed mb-4"
                            style={{ color: 'var(--ink-medium)', fontFamily: "'EB Garamond', serif" }}>
                            "{block.text}"
                          </p>
                          <p className="text-sm tracking-[0.3em] uppercase"
                            style={{ color: 'var(--ink-muted)' }}>— {block.source}</p>
                        </div>
                      )}
                    </motion.div>
                  </>
                ))}
              </div>
            )}

            <div ref={bottomRef} className="mt-24">
              <AnimatePresence>
                {scrollCompleted && !showQuiz && (
                  <motion.div
                    initial={{ opacity: 0, y: 30 }}
                    animate={{ opacity: 1, y: 0 }}
                    exit={{ opacity: 0 }}
                    transition={{ duration: 0.8 }}
                    className="text-center pt-16"
                    style={{ borderTop: '1px solid var(--parchment-border)' }}
                  >
                    {quizPassed ? (
                      <>
                        <p className="text-xl tracking-[0.4em] uppercase mb-3"
                          style={{ color: '#ca8a04', fontFamily: "'Cinzel', serif", fontWeight: '700' }}>
                          ★ Chapter Complete
                        </p>
                        <p className="text-lg mb-8"
                          style={{ color: 'var(--ink-medium)', fontFamily: "'EB Garamond', serif" }}>
                          You have mastered this chapter.
                        </p>
                        {chapter.hasQuiz && quizQuestions.length > 0 && !hasEarnedRare && (
                          <motion.button
                            whileHover={{ scale: 1.05 }}
                            whileTap={{ scale: 0.95 }}
                            onClick={() => { resetQuiz(); setShowQuiz(true) }}
                            className="px-8 py-4 text-sm tracking-widest uppercase cursor-pointer transition-all duration-300"
                            style={{
                              border: '1px solid #3b82f680',
                              color: '#60a5fa',
                              background: 'transparent',
                              fontFamily: "'Cinzel', serif"
                            }}
                          >
                            Retry for Rare ◈
                          </motion.button>
                        )}
                      </>
                    ) : (
                      <>
                        <motion.p
                          animate={{ opacity: [0.5, 1, 0.5] }}
                          transition={{ duration: 3, repeat: Infinity }}
                          className="text-sm tracking-[0.4em] uppercase mb-4"
                          style={{ color: 'var(--accent-amber)' }}
                        >
                          You have walked this trail
                        </motion.p>
                        <p className="text-lg mb-8"
                          style={{ color: 'var(--ink-light)', fontFamily: "'EB Garamond', serif" }}>
                          {chapter.hasQuiz
                            ? 'Prove your knowledge to earn your reward.'
                            : 'Chapter complete.'}
                        </p>
{chapter.hasQuiz && quizQuestions.length > 0 && (
  isAuthenticated ? (
    <motion.button
      whileHover={{ scale: 1.05 }}
      whileTap={{ scale: 0.95 }}
      onClick={() => setShowQuiz(true)}
      className="px-8 py-4 text-sm tracking-widest uppercase cursor-pointer transition-all duration-300"
      style={{
        border: '1px solid var(--accent-amber-dim)',
        color: 'var(--ink-dark)',
        background: 'var(--parchment-mid)',
        fontFamily: "'Cinzel', serif"
      }}
    >
      Test Your Knowledge →
    </motion.button>
  ) : (
    <motion.div
      initial={{ opacity: 0, y: 10 }}
      animate={{ opacity: 1, y: 0 }}
      className="max-w-sm mx-auto px-8 py-6 text-center"
      style={{
        background: 'var(--parchment-mid)',
        borderLeft: '3px solid var(--accent-amber)',
      }}
    >
      <p className="text-xs tracking-[0.3em] uppercase mb-3"
        style={{ color: 'var(--accent-amber)', fontFamily: "'Cinzel', serif" }}>
        Join The Trail
      </p>
      <p className="text-base mb-5 leading-relaxed"
        style={{ color: 'var(--ink-medium)', fontFamily: "'EB Garamond', serif" }}>
        Register to take the quiz, earn collectibles, and build your trophy cabinet.
      </p>
      <motion.button
        whileHover={{ scale: 1.05 }}
        whileTap={{ scale: 0.95 }}
        onClick={() => navigate('/register')}
        className="px-8 py-3 text-sm tracking-widest uppercase cursor-pointer transition-all duration-300"
        style={{
          background: 'var(--accent-amber)',
          color: 'var(--ink-dark)',
          border: 'none',
          fontFamily: "'Cinzel', serif",
          fontWeight: '700',
        }}
      >
        Create Account →
      </motion.button>
    </motion.div>
  )
)}
                      </>
                    )}
                  </motion.div>
                )}
              </AnimatePresence>
            </div>
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
            className="fixed inset-0 z-50 flex items-center justify-center px-4 backdrop-blur-sm"
            style={{ background: 'rgba(13,10,6,0.97)' }}
          >
            <motion.button
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ delay: 0.3 }}
              onClick={() => { setShowQuiz(false); resetQuiz() }}
              className="absolute top-6 right-6 text-sm tracking-widest uppercase cursor-pointer"
              style={{ color: 'var(--ink-muted)' }}
            >
              ✕ Close
            </motion.button>

            <div className="w-full max-w-2xl">
              <div className="flex items-center justify-between mb-10">
                <p className="text-sm tracking-widest uppercase" style={{ color: 'var(--ink-muted)' }}>
                  Question {currentQuestion + 1} of {quizQuestions.length}
                </p>
                <div className="flex gap-2">
                  {quizQuestions.map((_, i) => (
                    <div key={i} className="w-2.5 h-2.5 rounded-full transition-all duration-500"
                      style={{
                        background: i < currentQuestion
                          ? answers[i] === true ? '#4ade80' : '#f87171'
                          : i === currentQuestion
                          ? 'var(--accent-amber)'
                          : 'var(--parchment-border)'
                      }} />
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
                  <h2 className="text-3xl md:text-4xl font-bold mb-12 leading-relaxed"
                    style={{ color: 'var(--parchment-light)', fontFamily: "'Cinzel', serif" }}>
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
                          className="flex items-center gap-4 p-5 text-left transition-all duration-300"
                          style={getOptionStyle(option)}
                        >
                          <span className="text-sm tracking-widest shrink-0"
                            style={{ color: 'var(--accent-amber-dim)', fontFamily: "'Cinzel', serif" }}>
                            {option}
                          </span>
                          <span className="text-lg" style={{ fontFamily: "'EB Garamond', serif" }}>
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
                      className="text-center mt-6 text-base tracking-widest uppercase"
                      style={{ color: answeredCorrectly ? '#4ade80' : '#f87171' }}
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
            className="fixed inset-0 z-50 flex items-center justify-center px-4"
            style={{ background: 'rgba(13,10,6,0.97)' }}
          >
            <motion.div
              initial={{ scale: 0.9, opacity: 0 }}
              animate={{ scale: 1, opacity: 1 }}
              transition={{ delay: 0.2, duration: 0.5 }}
              className="text-center max-w-md"
            >
              <p className="text-sm tracking-[0.4em] uppercase mb-8"
                style={{ color: 'var(--ink-muted)' }}>Results</p>
              <p className="text-8xl font-bold mb-4"
                style={{
                  color: passed ? 'var(--accent-amber)' : 'var(--ink-muted)',
                  fontFamily: "'Cinzel', serif"
                }}>
                {score}/{quizQuestions.length}
              </p>

              {perfectScore && (
                <motion.p
                  initial={{ opacity: 0, y: -10 }}
                  animate={{ opacity: 1, y: 0 }}
                  transition={{ delay: 0.4 }}
                  className="text-sm tracking-[0.4em] uppercase mb-3"
                  style={{ color: '#3b82f6', fontFamily: "'Cinzel', serif" }}
                >
                  ◈ Perfect Score — Rare Collectible Earned
                </motion.p>
              )}

              <p className="text-2xl mb-3"
                style={{
                  color: passed ? 'var(--parchment-light)' : 'var(--ink-muted)',
                  fontFamily: "'Cinzel', serif"
                }}>
                {passed ? 'Excellent' : 'Not quite'}
              </p>
              <p className="text-lg mb-16"
                style={{ color: 'var(--ink-muted)', fontFamily: "'EB Garamond', serif" }}>
                {passed
                  ? 'You have proven your knowledge of this era.'
                  : 'The trail is long. Study and try again.'}
              </p>

              {/* Buttons — only shown on failure */}
              <div className="flex gap-4 justify-center">
                {!passed && (
                  <>
                    <motion.button
                      whileHover={{ scale: 1.05 }}
                      onClick={() => { resetQuiz(); setShowQuiz(true) }}
                      className="px-6 py-3 text-sm tracking-widest uppercase cursor-pointer transition-all"
                      style={{
                        border: '1px solid var(--parchment-border)',
                        color: 'var(--ink-muted)',
                        background: 'transparent'
                      }}
                    >
                      Try Again
                    </motion.button>
                    <motion.button
                      whileHover={{ scale: 1.05 }}
                      onClick={() => navigate(-1)}
                      className="px-6 py-3 text-sm tracking-widest uppercase cursor-pointer transition-all"
                      style={{
                        border: '1px solid var(--accent-amber-dim)',
                        color: 'var(--accent-amber)',
                        background: 'transparent'
                      }}
                    >
                      Return to Era
                    </motion.button>
                  </>
                )}
              </div>
            </motion.div>
          </motion.div>
        )}
      </AnimatePresence>

      {/* Reward screen */}
      <AnimatePresence>
        {showReward && (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            className="fixed inset-0 z-50 flex items-center justify-center px-4 backdrop-blur-xl"
            style={{
              background: earnedRare
                ? 'radial-gradient(ellipse at center, rgba(59,130,246,0.15) 0%, rgba(13,10,6,0.97) 60%)'
                : 'radial-gradient(ellipse at center, rgba(180,120,30,0.15) 0%, rgba(13,10,6,0.97) 60%)'
            }}
          >
            <div className="text-center max-w-sm">
              <motion.p
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                transition={{ delay: 0.3 }}
                className="text-sm tracking-[0.5em] uppercase mb-3"
                style={{ color: 'var(--accent-amber)', opacity: 0.7 }}
              >
                {earnedRare ? 'Rewards Unlocked' : 'Reward Unlocked'}
              </motion.p>

              <motion.div
                initial={{ scale: 0, opacity: 0, y: 40 }}
                animate={{ scale: 1, opacity: 1, y: 0 }}
                transition={{ delay: 0.5, duration: 0.8, ease: [0.32, 0.72, 0, 1] }}
                className="relative inline-block mb-6"
              >
                <motion.div
                  animate={{ opacity: [0.3, 0.7, 0.3], scale: [1, 1.15, 1] }}
                  transition={{ duration: 2.5, repeat: Infinity }}
                  className="absolute inset-0 rounded-full blur-2xl"
                  style={{ background: 'rgba(212,168,83,0.25)' }}
                />
                <div className="relative w-48 h-48 rounded-full overflow-hidden"
                  style={{
                    border: '2px solid var(--accent-amber)',
                    boxShadow: '0 0 40px rgba(212,168,83,0.3)'
                  }}>
                  <img
                    src={chapter.collectibleImageUrl ?? '/images/collectibles/trex.jpg'}
                    alt="Common Collectible"
                    className="w-full h-full object-cover"
                  />
                </div>
              </motion.div>

              <motion.div
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ delay: 0.9, duration: 0.6 }}
                className="px-6 py-5 mb-6"
                style={{
                  background: 'rgba(13,10,6,0.8)',
                  border: '1px solid var(--accent-amber-dim)',
                  backdropFilter: 'blur(8px)'
                }}
              >
                <p className="text-xl font-bold mb-1"
                  style={{ color: 'var(--parchment-light)', fontFamily: "'Cinzel', serif" }}>
                  {chapter.collectibleName ?? 'Collectible Earned'}
                </p>
                <p className="text-xs tracking-[0.3em] uppercase mb-3"
                  style={{ color: 'var(--ink-muted)' }}>
                  Common Collectible
                </p>
                <p className="text-sm leading-relaxed"
                  style={{ color: 'var(--parchment-dark)', fontFamily: "'EB Garamond', serif" }}>
                  {chapter.collectibleDescription ?? ''}
                </p>
              </motion.div>

              {earnedRare && (
                <>
                  <motion.div
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    transition={{ delay: 1.4 }}
                    className="flex items-center gap-3 mb-6"
                  >
                    <div className="h-px flex-1" style={{ backgroundColor: '#3b82f640' }} />
                    <span style={{ color: '#60a5fa', fontSize: '0.8rem', fontFamily: "'Cinzel', serif" }}>
                      ◈ PERFECT SCORE BONUS
                    </span>
                    <div className="h-px flex-1" style={{ backgroundColor: '#3b82f640' }} />
                  </motion.div>

                  <motion.div
                    initial={{ scale: 0, opacity: 0, y: 40 }}
                    animate={{ scale: 1, opacity: 1, y: 0 }}
                    transition={{ delay: 1.6, duration: 0.8, ease: [0.32, 0.72, 0, 1] }}
                    className="relative inline-block mb-6"
                  >
                    <motion.div
                      animate={{ opacity: [0.4, 0.9, 0.4], scale: [1, 1.2, 1] }}
                      transition={{ duration: 2.5, repeat: Infinity }}
                      className="absolute inset-0 rounded-full blur-2xl"
                      style={{ background: 'rgba(59,130,246,0.35)' }}
                    />
                    <div className="relative w-48 h-48 rounded-full overflow-hidden"
                      style={{
                        border: '2px solid #3b82f6',
                        boxShadow: '0 0 50px rgba(59,130,246,0.4)'
                      }}>
                      <img
                        src={chapter.rareCollectibleImageUrl ?? '/images/collectibles/amber-fossil.jpg'}
                        alt="Rare Collectible"
                        className="w-full h-full object-cover"
                      />
                    </div>
                  </motion.div>

                  <motion.div
                    initial={{ opacity: 0, y: 20 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ delay: 2, duration: 0.6 }}
                    className="px-6 py-5 mb-6"
                    style={{
                      background: 'rgba(13,10,6,0.8)',
                      border: '1px solid #3b82f660',
                      backdropFilter: 'blur(8px)'
                    }}
                  >
                    <p className="text-xl font-bold mb-1"
                      style={{ color: '#93c5fd', fontFamily: "'Cinzel', serif" }}>
                      {chapter.rareCollectibleName ?? 'Rare Collectible'}
                    </p>
                    <p className="text-xs tracking-[0.3em] uppercase mb-3"
                      style={{ color: '#3b82f6' }}>
                      Rare Collectible
                    </p>
                    <p className="text-sm leading-relaxed"
                      style={{ color: 'var(--parchment-dark)', fontFamily: "'EB Garamond', serif" }}>
                      {chapter.rareCollectibleDescription ?? ''}
                    </p>
                  </motion.div>
                </>
              )}

              {/* Continue button — becomes "Claim Your Legend" if legendary was awarded */}
              <motion.button
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                transition={{ delay: earnedRare ? 2.4 : 1.4 }}
                whileHover={{ scale: 1.05 }}
                onClick={() => {
                  if (grandmasterData) {
                    setShowReward(false)
                    setShowGrandmaster(true)
                  } else {
                    navigate(-1)
                  }
                }}
                className="px-8 py-3 text-sm tracking-widest uppercase cursor-pointer transition-all"
                style={{
                  border: '1px solid var(--accent-amber)',
                  color: grandmasterData ? '#1a0f05' : 'var(--accent-amber)',
                  background: grandmasterData ? '#d4a853' : 'transparent',
                  fontFamily: "'Cinzel', serif"
                }}
              >
                {grandmasterData ? 'Claim Your Legend →' : 'Continue The Trail →'}
              </motion.button>
            </div>
          </motion.div>
        )}
      </AnimatePresence>

      {/* Era Grandmaster celebration */}
      <EraGrandmasterModal
        show={showGrandmaster}
        eraName={grandmasterData?.eraName ?? ''}
        legendaryName={grandmasterData?.legendaryName ?? ''}
        legendaryDescription={grandmasterData?.legendaryDescription ?? ''}
        legendaryImageUrl={grandmasterData?.legendaryImageUrl ?? null}
      />

    </div>
  )
}