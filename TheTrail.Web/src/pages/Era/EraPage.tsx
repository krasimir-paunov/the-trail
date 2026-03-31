import { useEffect, useState } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { motion } from 'framer-motion'
import { erasApi } from '../../api/erasApi.ts'
import { chaptersApi } from '../../api/chaptersApi.ts'
import type { EraDto, ChapterDto } from '../../types/index.ts'

const eraThemes: Record<string, {
  bg: string
  accentClass: string
  accentColor: string
  text: string
  image?: string
}> = {
  prehistoric: {
    bg: 'from-stone-950 via-red-950 to-stone-900',
    accentClass: 'text-orange-400',
    accentColor: '#fb923c',
    text: 'text-stone-200',
    image: '/images/eras/PrehistoricEra.png'
  },
  ancient: {
    bg: 'from-yellow-950 via-amber-900 to-stone-900',
    accentClass: 'text-yellow-400',
    accentColor: '#facc15',
    text: 'text-amber-100',
    image: '/images/eras/AncientEra.png'
  },
  medieval: {
    bg: 'from-slate-950 via-red-900 to-slate-900',
    accentClass: 'text-red-400',
    accentColor: '#f87171',
    text: 'text-slate-200',
    image: '/images/eras/MedievalEra.png'
  },
  renaissance: {
    bg: 'from-indigo-950 via-purple-900 to-slate-900',
    accentClass: 'text-purple-300',
    accentColor: '#d8b4fe',
    text: 'text-indigo-100',
    image: '/images/eras/RenaissanceEra.png'
  },
  exploration: {
    bg: 'from-emerald-950 via-teal-900 to-stone-900',
    accentClass: 'text-emerald-400',
    accentColor: '#34d399',
    text: 'text-emerald-100',
    image: '/images/eras/ExplorationEra.png'
  },
  modern: {
    bg: 'from-zinc-900 via-zinc-800 to-stone-900',
    accentClass: 'text-zinc-300',
    accentColor: '#d4d4d8',
    text: 'text-zinc-100',
    image: '/images/eras/ModernEra.png'
  },
  digital: {
    bg: 'from-slate-950 via-blue-950 to-slate-900',
    accentClass: 'text-blue-400',
    accentColor: '#60a5fa',
    text: 'text-blue-100',
    image: '/images/eras/DigitalEra.png'
  },
}

const defaultTheme = {
  bg: 'from-stone-950 via-stone-900 to-stone-800',
  accentClass: 'text-stone-300',
  accentColor: '#d4a853',
  text: 'text-stone-100',
  image: undefined
}

export default function EraPage() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const [era, setEra] = useState<EraDto | null>(null)
  const [chapters, setChapters] = useState<ChapterDto[]>([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    if (!id) return
    const eraId = parseInt(id)
    Promise.all([
      erasApi.getById(eraId),
      chaptersApi.getByEra(eraId)
    ]).then(([eraData, chaptersData]) => {
      setEra(eraData)
      setChapters(chaptersData)
      setLoading(false)
    }).catch(() => navigate('/'))
  }, [id, navigate])

  const getTheme = (colorTheme: string | null) => {
    if (!colorTheme) return defaultTheme
    return eraThemes[colorTheme.toLowerCase()] ?? defaultTheme
  }

  if (loading || !era) {
    return (
      <div className="min-h-screen flex items-center justify-center"
        style={{ background: 'var(--hero-dark)' }}>
        <motion.div
          animate={{ opacity: [0.3, 1, 0.3] }}
          transition={{ duration: 2, repeat: Infinity }}
          className="text-xl tracking-widest uppercase"
          style={{ color: 'var(--accent-amber)' }}
        >
          Entering the era...
        </motion.div>
      </div>
    )
  }

  const theme = getTheme(era.colorTheme)
  const masteredCount = chapters.filter(c => c.quizPassed).length

  return (
    <div className="min-h-screen" style={{ background: 'var(--hero-dark)' }}>

      {/* ── Era Hero ── */}
      <div className="relative h-[75vh] flex flex-col items-center justify-end pb-20 px-4">
        {theme.image && (
          <img
            src={theme.image}
            alt={era.name}
            className="absolute inset-0 w-full h-full object-cover"
            style={{ objectPosition: '50% 30%' }}
          />
        )}
        <div className="absolute inset-0"
          style={{ background: 'linear-gradient(to top, var(--hero-dark) 0%, rgba(0,0,0,0.55) 50%, rgba(0,0,0,0.15) 100%)' }}
        />

        <motion.button
          initial={{ opacity: 0, x: -20 }}
          animate={{ opacity: 1, x: 0 }}
          transition={{ delay: 0.3 }}
          onClick={() => navigate('/')}
          className="absolute top-8 left-8 text-sm tracking-widest uppercase cursor-pointer transition-colors duration-300"
          style={{ color: 'var(--parchment-dark)', opacity: 0.7 }}
        >
          ← Back
        </motion.button>

        <div className="relative z-10 text-center max-w-4xl">
          <motion.p
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.2 }}
            className="text-sm tracking-[0.5em] uppercase mb-4"
            style={{ color: theme.accentColor }}
          >
            Era {era.order}
          </motion.p>
          <motion.h1
            initial={{ opacity: 0, y: 30 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.3 }}
            className="text-6xl md:text-9xl font-bold mb-8"
            style={{ color: 'var(--parchment-light)', fontFamily: "'Cinzel', serif", lineHeight: 1 }}
          >
            {era.name}
          </motion.h1>
          <motion.p
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.5 }}
            className="text-2xl leading-relaxed max-w-2xl mx-auto mb-12"
            style={{ color: 'var(--parchment-dark)', fontFamily: "'EB Garamond', serif" }}
          >
            {era.description}
          </motion.p>

          {/* Scroll indicator */}
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 1 }}
            className="flex flex-col items-center gap-2"
          >
            <p className="text-xs tracking-[0.4em] uppercase"
              style={{ color: theme.accentColor, opacity: 0.6 }}>
              Begin the journey
            </p>
            <motion.div
              animate={{ y: [0, 6, 0] }}
              transition={{ duration: 1.8, repeat: Infinity }}
              style={{ color: theme.accentColor, opacity: 0.6, fontSize: '1.2rem' }}
            >
              ↓
            </motion.div>
          </motion.div>
        </div>
      </div>

      {/* ── Chapter List ── */}
      <div className="parchment">
        <div className="max-w-4xl mx-auto px-8 py-16">

          {/* Header row */}
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.6 }}
            className="flex items-center justify-between mb-12 pb-6"
            style={{ borderBottom: '1px solid var(--parchment-border)' }}
          >
            <div>
              <p className="text-xs tracking-[0.4em] uppercase mb-1"
                style={{ color: 'var(--ink-muted)' }}>
                {era.name}
              </p>
              <p className="text-2xl font-bold"
                style={{ color: 'var(--ink-dark)', fontFamily: "'Cinzel', serif" }}>
                {chapters.length} {chapters.length === 1 ? 'Chapter' : 'Chapters'}
              </p>
            </div>

            {/* Progress summary */}
            <div className="flex items-center gap-3">
              {[...Array(chapters.length)].map((_, i) => (
                <div
                  key={i}
                  className="w-2 h-2 rounded-full transition-all duration-500"
                  style={{
                    background: i < masteredCount ? theme.accentColor : 'transparent',
                    border: `1.5px solid ${i < masteredCount ? theme.accentColor : 'var(--parchment-border)'}`,
                  }}
                />
              ))}
              <p className="text-sm ml-2"
                style={{ color: 'var(--ink-muted)', fontFamily: "'Cinzel', serif" }}>
                {masteredCount}/{chapters.length}
              </p>
            </div>
          </motion.div>

          {/* Grandmaster banner */}
          {era.isGrandmasterUnlocked && (
            <motion.div
              initial={{ opacity: 0, y: -10 }}
              animate={{ opacity: 1, y: 0 }}
              className="flex items-center justify-center gap-4 mb-10 py-4 px-6"
              style={{
                background: 'rgba(202,138,4,0.08)',
                border: '1px solid rgba(202,138,4,0.3)',
              }}
            >
              <motion.span
                animate={{ opacity: [0.6, 1, 0.6] }}
                transition={{ duration: 2, repeat: Infinity }}
                style={{ color: '#ca8a04', fontSize: '1.1rem' }}
              >
                ★
              </motion.span>
              <p className="text-sm tracking-[0.3em] uppercase font-bold"
                style={{ color: '#ca8a04', fontFamily: "'Cinzel', serif" }}>
                Era Grandmaster Unlocked
              </p>
              <motion.span
                animate={{ opacity: [0.6, 1, 0.6] }}
                transition={{ duration: 2, repeat: Infinity }}
                style={{ color: '#ca8a04', fontSize: '1.1rem' }}
              >
                ★
              </motion.span>
            </motion.div>
          )}

          {chapters.length === 0 ? (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ delay: 0.7 }}
              className="text-center py-24"
              style={{ border: '1px solid var(--parchment-border)' }}
            >
              <p className="text-4xl mb-6" style={{ color: theme.accentColor }}>✦</p>
              <p className="text-2xl mb-3"
                style={{ color: 'var(--ink-dark)', fontFamily: "'Cinzel', serif" }}>
                Chapters coming soon
              </p>
              <p className="text-lg"
                style={{ color: 'var(--ink-muted)', fontFamily: "'EB Garamond', serif" }}>
                This era is being curated. Check back soon.
              </p>
            </motion.div>
          ) : (
            <div className="flex flex-col gap-3">
              {chapters.map((chapter, index) => {
                const isFullyMastered = chapter.quizPassed

                return (
                  <motion.div
                    key={chapter.id}
                    initial={{ opacity: 0, y: 20 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ delay: 0.7 + index * 0.08 }}
                    onClick={() => navigate(`/chapters/${chapter.id}`)}
                    className="relative overflow-hidden cursor-pointer transition-all duration-300 group"
                    style={{
                      border: '1px solid var(--parchment-border)',
                      borderLeft: isFullyMastered
                        ? `3px solid ${theme.accentColor}`
                        : '1px solid var(--parchment-border)',
                      background: 'var(--parchment-light)',
                    }}
                    whileHover={{
                      borderColor: theme.accentColor,
                      borderLeftColor: theme.accentColor,
                    }}
                  >
                    {/* Watermark chapter number */}
                    <div
                      className="absolute right-4 top-1/2 -translate-y-1/2 font-bold select-none pointer-events-none"
                      style={{
                        fontSize: '5rem',
                        color: theme.accentColor,
                        opacity: 0.045,
                        fontFamily: "'Cinzel', serif",
                        lineHeight: 1,
                      }}
                    >
                      {String(chapter.order).padStart(2, '0')}
                    </div>

                    <div className="p-6 flex items-center justify-between relative z-10">
                      <div className="flex items-center gap-6">
                        {/* Chapter number */}
                        <p className="text-3xl font-bold shrink-0"
                          style={{
                            color: theme.accentColor,
                            opacity: isFullyMastered ? 0.9 : 0.35,
                            fontFamily: "'Cinzel', serif",
                            transition: 'opacity 0.3s'
                          }}>
                          {String(chapter.order).padStart(2, '0')}
                        </p>

                        <div>
                          <h3 className="text-xl font-bold mb-1"
                            style={{
                              color: 'var(--ink-dark)',
                              fontFamily: "'Cinzel', serif"
                            }}>
                            {chapter.title}
                          </h3>
                          <p className="text-base mb-2"
                            style={{
                              color: 'var(--ink-light)',
                              fontFamily: "'EB Garamond', serif"
                            }}>
                            {chapter.subtitle}
                          </p>

                          {/* Badges row */}
                          <div className="flex items-center gap-2">
                            <span className="text-xs tracking-wider px-2 py-0.5"
                              style={{
                                color: 'var(--ink-muted)',
                                border: '1px solid var(--parchment-border)',
                                fontFamily: "'Cinzel', serif"
                              }}>
                              {chapter.estimatedMinutes} min
                            </span>
                            {chapter.hasQuiz && (
                              <span className="text-xs tracking-wider px-2 py-0.5"
                                style={{
                                  color: theme.accentColor,
                                  border: `1px solid ${theme.accentColor}40`,
                                  fontFamily: "'Cinzel', serif"
                                }}>
                                Quiz
                              </span>
                            )}
                            {chapter.hasCollectible && (
                              <span className="text-xs tracking-wider px-2 py-0.5"
                                style={{
                                  color: '#ca8a04',
                                  border: '1px solid rgba(202,138,4,0.3)',
                                  fontFamily: "'Cinzel', serif"
                                }}>
                                Collectible
                              </span>
                            )}
                          </div>
                        </div>
                      </div>

                      {/* Right side — progress indicators */}
                      <div className="flex items-center gap-5 shrink-0">

                        {/* Read dot */}
                        <div className="flex flex-col items-center gap-1">
                          <div className="w-2.5 h-2.5 rounded-full border-2 transition-all duration-500"
                            style={{
                              background: chapter.scrollCompleted ? 'var(--accent-amber)' : 'transparent',
                              borderColor: chapter.scrollCompleted ? 'var(--accent-amber)' : 'var(--parchment-border)'
                            }} />
                          <p className="text-xs" style={{ color: 'var(--ink-muted)', fontFamily: "'Cinzel', serif" }}>
                            Read
                          </p>
                        </div>

                        {/* Common collectible dot */}
                        <div className="flex flex-col items-center gap-1">
                          <div className="w-2.5 h-2.5 rounded-full border-2 transition-all duration-500"
                            style={{
                              background: chapter.commonCollectibleEarned ? '#d4a853' : 'transparent',
                              borderColor: chapter.commonCollectibleEarned ? '#d4a853' : 'var(--parchment-border)'
                            }} />
                          <p className="text-xs" style={{ color: 'var(--ink-muted)', fontFamily: "'Cinzel', serif" }}>
                            Common
                          </p>
                        </div>

                        {/* Rare collectible diamond */}
                        <div className="flex flex-col items-center gap-1">
                          <div
                            className="w-2.5 h-2.5 transition-all duration-500"
                            style={{
                              background: chapter.rareCollectibleEarned ? '#3b82f6' : 'transparent',
                              border: `2px solid ${chapter.rareCollectibleEarned ? '#3b82f6' : 'var(--parchment-border)'}`,
                              transform: 'rotate(45deg)',
                            }}
                          />
                          <p className="text-xs" style={{ color: 'var(--ink-muted)', fontFamily: "'Cinzel', serif" }}>
                            Rare
                          </p>
                        </div>

                        {/* Enter arrow */}
                        <p className="text-sm tracking-widest uppercase opacity-0 group-hover:opacity-100 transition-opacity duration-300 ml-2"
                          style={{ color: theme.accentColor, fontFamily: "'Cinzel', serif" }}>
                          Enter →
                        </p>
                      </div>
                    </div>
                  </motion.div>
                )
              })}
            </div>
          )}
        </div>
      </div>

    </div>
  )
}