import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { motion } from 'framer-motion'
import { erasApi } from '../../api/erasApi.ts'
import type { EraDto } from '../../types/index.ts'

const eraThemes: Record<string, {
  bg: string
  accent: string
  text: string
  description: string
  image?: string
}> = {
  prehistoric: {
    bg: 'from-stone-950 via-red-950 to-stone-900',
    accent: 'text-orange-400',
    text: 'text-stone-200',
    description: 'The dawn of life itself',
    image: '/images/eras/PrehistoricEra.png'
  },
  ancient: {
    bg: 'from-yellow-950 via-amber-900 to-stone-900',
    accent: 'text-yellow-400',
    text: 'text-amber-100',
    description: 'Empires of gold and sand',
    image: '/images/eras/AncientEra.png'
  },
  medieval: {
    bg: 'from-slate-950 via-red-900 to-slate-900',
    accent: 'text-red-400',
    text: 'text-slate-200',
    description: 'Iron, faith and fire',
    image: '/images/eras/MedievalEra.png'
  },
  renaissance: {
    bg: 'from-indigo-950 via-purple-900 to-slate-900',
    accent: 'text-purple-300',
    text: 'text-indigo-100',
    description: 'The rebirth of human thought',
    image: '/images/eras/RenaissanceEra.png'
  },
  exploration: {
    bg: 'from-emerald-950 via-teal-900 to-stone-900',
    accent: 'text-emerald-400',
    text: 'text-emerald-100',
    description: 'Beyond the edge of the known world',
    image: '/images/eras/ExplorationEra.png'
  },
  modern: {
    bg: 'from-zinc-900 via-zinc-800 to-stone-900',
    accent: 'text-zinc-300',
    text: 'text-zinc-100',
    description: 'The world remade',
    image: '/images/eras/ModernEra.png'
  },
  digital: {
    bg: 'from-slate-950 via-blue-950 to-slate-900',
    accent: 'text-blue-400',
    text: 'text-blue-100',
    description: 'The age of information',
    image: '/images/eras/DigitalEra.png'
  },
}

const defaultTheme = {
  bg: 'from-stone-950 via-stone-900 to-stone-800',
  accent: 'text-stone-300',
  text: 'text-stone-100',
  description: 'A new chapter awaits',
  image: undefined
}

export default function HomePage() {
  const [eras, setEras] = useState<EraDto[]>([])
  const [loading, setLoading] = useState(true)
  const [activeEra, setActiveEra] = useState<number | null>(null)
  const navigate = useNavigate()

  useEffect(() => {
    erasApi.getAll().then((data) => {
      setEras(data)
      setLoading(false)
    })
  }, [])

  const getTheme = (colorTheme: string | null) => {
    if (!colorTheme) return defaultTheme
    return eraThemes[colorTheme.toLowerCase()] ?? defaultTheme
  }

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center"
        style={{ background: 'var(--hero-dark)' }}>
        <motion.div
          animate={{ opacity: [0.3, 1, 0.3] }}
          transition={{ duration: 2, repeat: Infinity }}
          className="text-xl tracking-widest uppercase"
          style={{ color: 'var(--accent-amber)' }}
        >
          The Trail awaits...
        </motion.div>
      </div>
    )
  }

  return (
    <div className="min-h-screen" style={{ background: 'var(--hero-dark)' }}>

      {/* ── Hero Section ── */}
      <motion.div
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ duration: 1.5 }}
        className="relative h-screen flex flex-col items-center justify-center text-center px-4"
      >
        <div className="absolute inset-0">
          <img
            src="/images/HeroMap.jpg"
            alt="Ancient map"
            className="w-full h-full object-cover"
            style={{ objectPosition: '50% 40%' }}
          />
          <div className="absolute inset-0 bg-black/60" />
          <div
            className="absolute inset-0"
            style={{
              background: 'radial-gradient(ellipse at center, transparent 30%, rgba(0,0,0,0.8) 100%)'
            }}
          />
        </div>

        <motion.p
          initial={{ opacity: 0, y: -20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.5, duration: 1 }}
          className="relative z-10 text-sm tracking-[0.4em] uppercase mb-6"
          style={{ color: 'var(--accent-amber)', opacity: 0.7 }}
        >
          An interactive history encyclopedia
        </motion.p>

        <motion.h1
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.8, duration: 1 }}
          className="relative z-10 text-6xl md:text-8xl font-bold mb-6"
          style={{ color: 'var(--parchment-light)', fontFamily: "'Cinzel', serif" }}
        >
          The Trail
        </motion.h1>

        <motion.p
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 1.2, duration: 1 }}
          className="relative z-10 text-lg md:text-xl mb-12 tracking-wide"
          style={{ color: 'var(--parchment-dark)' }}
        >
          Follow the trail of human history
        </motion.p>

        <motion.button
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 1.6, duration: 0.8 }}
          whileHover={{ scale: 1.05 }}
          whileTap={{ scale: 0.95 }}
          onClick={() => {
            document.getElementById('eras')?.scrollIntoView({ behavior: 'smooth' })
          }}
          className="relative z-10 px-8 py-4 text-sm tracking-widest uppercase transition-all duration-300"
          style={{
            border: '1px solid var(--accent-amber-dim)',
            color: 'var(--parchment-light)',
            fontFamily: "'Cinzel', serif"
          }}
        >
          Begin Your Journey
        </motion.button>

        <motion.div
          animate={{ y: [0, 8, 0] }}
          transition={{ duration: 2, repeat: Infinity }}
          className="absolute bottom-8 z-10 text-xs tracking-widest uppercase"
          style={{ color: 'var(--accent-amber)', opacity: 0.4 }}
        >
          Scroll to explore
        </motion.div>
      </motion.div>

      {/* ── Era Accordion ── */}
      <div id="eras">
        <motion.h2
          initial={{ opacity: 0 }}
          whileInView={{ opacity: 1 }}
          viewport={{ once: true }}
          className="text-center text-base tracking-[0.6em] uppercase py-8"
          style={{
            color: 'var(--parchment-dark)',
            borderTop: '1px solid rgba(196,164,122,0.2)'
          }}
        >
          Choose Your Era
        </motion.h2>

        <div className="flex overflow-hidden" style={{ height: '700px' }}>
          {eras.map((era) => {
            const theme = getTheme(era.colorTheme)
            const isActive = activeEra === era.id
            const isAnyActive = activeEra !== null

            return (
              <motion.div
                key={era.id}
                animate={{
                  flexGrow: isActive ? 4 : isAnyActive ? 0.6 : 1
                }}
                transition={{ duration: 0.6, ease: [0.32, 0.72, 0, 1] }}
                onHoverStart={() => setActiveEra(era.id)}
                onHoverEnd={() => setActiveEra(null)}
                onClick={() => navigate(`/eras/${era.id}`)}
                className="relative overflow-hidden cursor-pointer flex-1"
              >
                {theme.image ? (
                  <motion.img
                    src={theme.image}
                    alt={era.name}
                    animate={{ scale: isActive ? 1.08 : 1 }}
                    transition={{ duration: 0.6 }}
                    className="absolute inset-0 w-full h-full object-cover"
                    style={{ objectPosition: '50% 30%' }}
                  />
                ) : (
                  <div className={`absolute inset-0 bg-linear-to-b ${theme.bg}`} />
                )}

                <motion.div
                  animate={{ opacity: isActive ? 0.3 : 0.65 }}
                  transition={{ duration: 0.4 }}
                  className="absolute inset-0 bg-black"
                />

                <div className="absolute inset-0 bg-linear-to-t from-black/80 via-transparent to-transparent" />

                <div className="absolute inset-0 flex flex-col justify-between p-6">
                  <p className={`text-xs tracking-[0.25em] uppercase ${theme.accent}`}>
                    Era {era.order}
                  </p>

                  <div>
                    <motion.h3
                      animate={{
                        writingMode: isActive ? 'horizontal-tb' : 'vertical-rl',
                        fontSize: isActive ? '2rem' : '1rem'
                      }}
                      transition={{ duration: 0.5 }}
                      className={`font-bold mb-2 ${theme.text}`}
                      style={{
                        writingMode: isActive ? 'horizontal-tb' : 'vertical-rl',
                        textOrientation: 'mixed',
                        transform: isActive ? 'none' : 'rotate(180deg)',
                        fontFamily: "'Cinzel', serif"
                      }}
                    >
                      {era.name}
                    </motion.h3>

                    <motion.div
                      animate={{ opacity: isActive ? 1 : 0, height: isActive ? 'auto' : 0 }}
                      transition={{ duration: 0.3 }}
                      className="overflow-hidden"
                    >
                      <p className={`text-base mb-3 opacity-80 ${theme.text}`}
                        style={{ fontFamily: "'EB Garamond', serif" }}>
                        {theme.description}
                      </p>
                      <div className="flex items-center justify-between">
                        <p className={`text-sm ${theme.accent}`}>
                          {era.chapterCount} {era.chapterCount === 1 ? 'chapter' : 'chapters'}
                        </p>
                        <p className={`text-sm tracking-widest uppercase ${theme.accent}`}>
                          Enter →
                        </p>
                      </div>
                      {era.isGrandmasterUnlocked && (
                        <p className="text-sm text-yellow-400 mt-2">★ Grandmaster</p>
                      )}
                    </motion.div>
                  </div>
                </div>
              </motion.div>
            )
          })}
        </div>
      </div>

      {/* ── How It Works ── */}
      <div className="parchment">
        <div className="max-w-4xl mx-auto px-4 py-24">
          <motion.p
            initial={{ opacity: 0 }}
            whileInView={{ opacity: 1 }}
            viewport={{ once: true }}
            className="text-center text-xs tracking-[0.5em] uppercase mb-4"
            style={{ color: 'var(--accent-amber-dim)' }}
          >
            The Journey
          </motion.p>

          <motion.h2
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ delay: 0.1, duration: 0.9 }}
            className="text-center text-4xl font-bold mb-20"
            style={{
              color: 'var(--ink-dark)',
              fontFamily: "'Cinzel', serif"
            }}
          >
            How The Trail Works
          </motion.h2>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-16">
            {[
              {
                number: 'I',
                title: 'Choose Your Era',
                description: 'Select from seven distinct eras of human history. Each era is a world unto itself — visually unique, historically rich.'
              },
              {
                number: 'II',
                title: 'Follow The Trail',
                description: 'Explore chapters within each era. Read the stories, study the events, absorb the knowledge at your own pace.'
              },
              {
                number: 'III',
                title: 'Earn Your Place',
                description: 'Complete quizzes to earn collectibles and era badges. Prove your knowledge. Build your trophy cabinet.'
              },
            ].map((step, i) => (
              <motion.div
                key={step.number}
                initial={{ opacity: 0, y: 30 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true }}
                transition={{ delay: i * 0.2, duration: 0.9 }}
                className="text-center"
              >
                <p className="text-6xl font-bold mb-6"
                  style={{
                    color: 'var(--accent-amber)',
                    fontFamily: "'Cinzel', serif",
                    opacity: 0.5
                  }}>
                  {step.number}
                </p>
                <div className="w-px h-14 mx-auto mb-6"
                  style={{ background: 'var(--parchment-border)' }} />
                <h3 className="text-xl font-bold mb-4 tracking-wide"
                  style={{
                    color: 'var(--ink-dark)',
                    fontFamily: "'Cinzel', serif"
                  }}>
                  {step.title}
                </h3>
                <p className="text-lg leading-relaxed"
                  style={{
                    color: 'var(--ink-light)',
                    fontFamily: "'EB Garamond', serif"
                  }}>
                  {step.description}
                </p>
              </motion.div>
            ))}
          </div>
        </div>
      </div>

    </div>
  )
}