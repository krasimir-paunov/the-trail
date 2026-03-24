import { useEffect, useState } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { motion } from 'framer-motion'
import { erasApi } from '../../api/erasApi.ts'
import { chaptersApi } from '../../api/chaptersApi.ts'
import type { EraDto, ChapterDto } from '../../types/index.ts'

const eraThemes: Record<string, {
  bg: string
  accent: string
  text: string
  image?: string
}> = {
  prehistoric: {
    bg: 'from-stone-950 via-red-950 to-stone-900',
    accent: 'text-orange-400',
    text: 'text-stone-200',
    image: '/images/eras/PrehistoricEra.png'
  },
  ancient: {
    bg: 'from-yellow-950 via-amber-900 to-stone-900',
    accent: 'text-yellow-400',
    text: 'text-amber-100',
    image: '/images/eras/AncientEra.png'
  },
  medieval: {
    bg: 'from-slate-950 via-red-900 to-slate-900',
    accent: 'text-red-400',
    text: 'text-slate-200',
    image: '/images/eras/MedievalEra.png'
  },
  renaissance: {
    bg: 'from-indigo-950 via-purple-900 to-slate-900',
    accent: 'text-purple-300',
    text: 'text-indigo-100',
    image: '/images/eras/RenaissanceEra.png'
  },
  modern: {
    bg: 'from-zinc-900 via-zinc-800 to-stone-900',
    accent: 'text-zinc-300',
    text: 'text-zinc-100',
    image: '/images/eras/ModernEra.png'
  },
  digital: {
    bg: 'from-slate-950 via-blue-950 to-slate-900',
    accent: 'text-blue-400',
    text: 'text-blue-100',
    image: '/images/eras/DigitalEra.png'
  },
}

const defaultTheme = {
  bg: 'from-stone-950 via-stone-900 to-stone-800',
  accent: 'text-stone-300',
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
    }).catch(() => {
      navigate('/')
    })
  }, [id, navigate])

  const getTheme = (colorTheme: string | null) => {
    if (!colorTheme) return defaultTheme
    return eraThemes[colorTheme.toLowerCase()] ?? defaultTheme
  }

  if (loading || !era) {
    return (
      <div className="min-h-screen bg-stone-950 flex items-center justify-center">
        <motion.div
          animate={{ opacity: [0.3, 1, 0.3] }}
          transition={{ duration: 2, repeat: Infinity }}
          className="text-stone-400 text-xl tracking-widest uppercase"
        >
          Entering the era...
        </motion.div>
      </div>
    )
  }

  const theme = getTheme(era.colorTheme)

  return (
    <div className="min-h-screen bg-stone-950">
      {/* Era Hero */}
      <div className="relative h-[60vh] flex flex-col items-center justify-end pb-16 px-4">
        {/* Background */}
        {theme.image && (
          <img
            src={theme.image}
            alt={era.name}
            className="absolute inset-0 w-full h-full object-cover"
            style={{ objectPosition: '50% 30%' }}
          />
        )}
        <div className="absolute inset-0 bg-linear-to-t from-stone-950 via-black/50 to-black/30" />

        {/* Back button */}
        <motion.button
          initial={{ opacity: 0, x: -20 }}
          animate={{ opacity: 1, x: 0 }}
          transition={{ delay: 0.3 }}
          onClick={() => navigate('/')}
          className="absolute top-8 left-8 text-stone-400 hover:text-amber-200 transition-colors text-sm tracking-widest uppercase cursor-pointer"
        >
          ← Back
        </motion.button>

        {/* Era info */}
        <div className="relative z-10 text-center max-w-3xl">
          <motion.p
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.2 }}
            className={`text-xs tracking-[0.4em] uppercase mb-3 ${theme.accent}`}
          >
            Era {era.order}
          </motion.p>
          <motion.h1
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.3 }}
            className={`text-5xl md:text-7xl font-bold mb-4 ${theme.text}`}
          >
            {era.name}
          </motion.h1>
          <motion.p
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.5 }}
            className="text-stone-400 text-base leading-relaxed"
          >
            {era.description}
          </motion.p>
        </div>
      </div>

      {/* Chapters */}
      <div className="max-w-4xl mx-auto px-4 py-16">
        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.6 }}
          className="flex items-center justify-between mb-12"
        >
          <p className="text-stone-500 text-xs tracking-[0.4em] uppercase">
            {chapters.length} {chapters.length === 1 ? 'Chapter' : 'Chapters'}
          </p>
          {era.isGrandmasterUnlocked && (
            <p className="text-yellow-400 text-xs tracking-widest uppercase">
              ★ Grandmaster Unlocked
            </p>
          )}
        </motion.div>

        {chapters.length === 0 ? (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.7 }}
            className="text-center py-24 border border-stone-800"
          >
            <p className={`text-4xl mb-4 ${theme.accent}`}>✦</p>
            <p className="text-stone-400 text-lg mb-2">Chapters coming soon</p>
            <p className="text-stone-600 text-m">
              This era is being curated. Check back soon.
            </p>
          </motion.div>
        ) : (
          <div className="flex flex-col gap-4">
            {chapters.map((chapter, index) => (
              <motion.div
                key={chapter.id}
                initial={{ opacity: 0, y: 20 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ delay: 0.7 + index * 0.1 }}
                onClick={() => navigate(`/chapters/${chapter.id}`)}
                className="relative overflow-hidden border border-stone-800 hover:border-stone-600 cursor-pointer transition-all duration-300 group"
              >
                {/* Chapter image if available */}
                {chapter.coverImageUrl && (
                  <img
                    src={chapter.coverImageUrl}
                    alt={chapter.title}
                    className="absolute inset-0 w-full h-full object-cover opacity-20 group-hover:opacity-30 transition-opacity duration-300"
                  />
                )}

                <div className="relative z-10 p-6 flex items-center justify-between">
                  <div className="flex items-center gap-6">
                    {/* Chapter number */}
                    <p className={`text-2xl font-bold opacity-30 ${theme.accent}`}>
                      {String(chapter.order).padStart(2, '0')}
                    </p>

                    {/* Chapter info */}
                    <div>
                      <h3 className="text-stone-200 text-lg font-bold mb-1 group-hover:text-amber-50 transition-colors">
                        {chapter.title}
                      </h3>
                      <p className="text-stone-500 text-sm">
                        {chapter.subtitle}
                      </p>
                    </div>
                  </div>

                  <div className="flex items-center gap-6 shrink-0">
                    {/* Meta */}
                    <div className="text-right hidden md:block">
                      <p className="text-stone-600 text-xs mb-1">
                        {chapter.estimatedMinutes} min read
                      </p>
                      <div className="flex items-center gap-2 justify-end">
                        {chapter.hasQuiz && (
                          <span className={`text-xs ${theme.accent}`}>Quiz</span>
                        )}
                        {chapter.hasCollectible && (
                          <span className="text-xs text-yellow-500">Collectible</span>
                        )}
                      </div>
                    </div>

                    {/* Progress */}
                    <div className="flex flex-col items-center gap-1">
                      <div className={`w-2 h-2 rounded-full border ${
                        chapter.scrollCompleted
                          ? 'bg-amber-200 border-amber-200'
                          : 'border-stone-600'
                      }`} />
                      <div className={`w-2 h-2 rounded-full border ${
                        chapter.quizPassed
                          ? 'bg-yellow-400 border-yellow-400'
                          : 'border-stone-600'
                      }`} />
                    </div>

                    <p className={`text-xs tracking-widest uppercase opacity-0 group-hover:opacity-100 transition-opacity ${theme.accent}`}>
                      Enter →
                    </p>
                  </div>
                </div>
              </motion.div>
            ))}
          </div>
        )}
      </div>
    </div>
  )
}