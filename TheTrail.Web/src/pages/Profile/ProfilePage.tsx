import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { motion, AnimatePresence } from 'framer-motion'
import { useAuth } from '../../context/AuthContext.tsx'
import apiClient from '../../api/client.ts'

interface CollectibleDto {
  id: number
  name: string
  description: string
  imageUrl: string
  rarity: string
  isEarned: boolean
  chapterId: number
}

interface EraProgressDto {
  eraId: number
  eraName: string
  colorTheme: string
  totalChapters: number
  completedChapters: number
  isGrandmasterUnlocked: boolean
}

interface ProfileDto {
  displayName: string
  email: string
  chaptersRead: number
  quizzesPassed: number
  eraProgress: EraProgressDto[]
  earnedCollectibles: CollectibleDto[]
  allCollectibles: CollectibleDto[]
}

const eraImages: Record<string, string> = {
  prehistoric: '/images/eras/PrehistoricEra.png',
  ancient: '/images/eras/AncientEra.png',
  medieval: '/images/eras/MedievalEra.png',
  renaissance: '/images/eras/RenaissanceEra.png',
  modern: '/images/eras/ModernEra.png',
  digital: '/images/eras/DigitalEra.png',
}

const eraAccents: Record<string, string> = {
  prehistoric: 'text-orange-400',
  ancient: 'text-yellow-400',
  medieval: 'text-red-400',
  renaissance: 'text-purple-300',
  modern: 'text-zinc-300',
  digital: 'text-blue-400',
}

const rarityBorder: Record<string, string> = {
  Common: 'border-stone-500',
  Rare: 'border-blue-500',
  Legendary: 'border-yellow-400',
}

const rarityGlowColor: Record<string, string> = {
  Common: 'rgba(120,110,90,0.4)',
  Rare: 'rgba(59,130,246,0.4)',
  Legendary: 'rgba(250,204,21,0.4)',
}

const rarityText: Record<string, string> = {
  Common: 'text-stone-400',
  Rare: 'text-blue-400',
  Legendary: 'text-yellow-400',
}

export default function ProfilePage() {
  const { isAuthenticated } = useAuth()
  const navigate = useNavigate()
  const [profile, setProfile] = useState<ProfileDto | null>(null)
  const [loading, setLoading] = useState(true)
  const [selectedCollectible, setSelectedCollectible] = useState<CollectibleDto | null>(null)

  useEffect(() => {
    if (!isAuthenticated) {
      navigate('/login')
      return
    }

    apiClient.get<ProfileDto>('/api/profile')
      .then(res => {
        setProfile(res.data)
        setLoading(false)
      })
      .catch(() => navigate('/'))
  }, [isAuthenticated, navigate])

  if (loading || !profile) {
    return (
      <div className="min-h-screen bg-stone-950 flex items-center justify-center">
        <motion.div
          animate={{ opacity: [0.3, 1, 0.3] }}
          transition={{ duration: 2, repeat: Infinity }}
          className="text-amber-200/50 text-xl tracking-widest uppercase"
        >
          Loading your trail...
        </motion.div>
      </div>
    )
  }

  return (
    <div className="min-h-screen bg-stone-950">

      {/* Hero */}
      <div className="relative min-h-[50vh] flex flex-col justify-end pb-16">
        <div className="absolute inset-0">
          <img
            src="/images/HeroMap.jpg"
            alt=""
            className="w-full h-full object-cover"
            style={{ objectPosition: '50% 40%' }}
          />
          <div className="absolute inset-0 bg-black/75" />
          <div
            className="absolute inset-0"
            style={{ background: 'radial-gradient(ellipse at center, transparent 20%, rgba(0,0,0,0.8) 100%)' }}
          />
          <div className="absolute inset-0 bg-linear-to-t from-stone-950 via-transparent to-transparent" />
        </div>

        <div className="relative z-10 max-w-6xl mx-auto px-8 w-full">
          <motion.p
            initial={{ opacity: 0, y: -10 }}
            animate={{ opacity: 1, y: 0 }}
            className="text-amber-200/40 text-xs tracking-[0.5em] uppercase mb-4"
          >
            Explorer Profile
          </motion.p>
          <motion.h1
            initial={{ opacity: 0, y: 10 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.1 }}
            className="text-6xl md:text-8xl font-bold text-amber-50 mb-3"
          >
            {profile.displayName}
          </motion.h1>
          <motion.p
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.2 }}
            className="text-stone-400 text-lg mb-10"
          >
            {profile.email}
          </motion.p>

          <motion.div
            initial={{ opacity: 0, y: 10 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.3 }}
            className="flex gap-16 border-t border-stone-800 pt-8"
          >
            {[
              { value: profile.chaptersRead, label: 'Chapters Read' },
              { value: profile.quizzesPassed, label: 'Quizzes Passed' },
              { value: profile.earnedCollectibles.length, label: 'Collectibles' },
              { value: profile.eraProgress.filter(e => e.isGrandmasterUnlocked).length, label: 'Eras Mastered' },
            ].map((stat, i) => (
              <div key={i}>
                <p className="text-amber-200 text-5xl font-bold mb-2">{stat.value}</p>
                <p className="text-stone-500 text-sm tracking-[0.3em] uppercase">{stat.label}</p>
              </div>
            ))}
          </motion.div>
        </div>
      </div>

      <div className="max-w-6xl mx-auto px-8 py-24 flex flex-col gap-28">

        {/* ── Trophy Cabinet ── */}
        <section>
          <motion.div
            initial={{ opacity: 0 }}
            whileInView={{ opacity: 1 }}
            viewport={{ once: true }}
            className="flex items-center gap-4 mb-4"
          >
            <p className="text-amber-200/50 text-sm tracking-[0.5em] uppercase">
              Trophy Cabinet
            </p>
            <div className="h-px flex-1 bg-stone-800" />
            <p className="text-stone-500 text-base">
              {profile.earnedCollectibles.length}
              <span className="text-stone-700">/{profile.allCollectibles.length}</span>
            </p>
          </motion.div>

          <motion.p
            initial={{ opacity: 0 }}
            whileInView={{ opacity: 1 }}
            viewport={{ once: true }}
            className="text-stone-500 text-base mb-16"
          >
            Complete chapters and pass quizzes to expand your collection.
          </motion.p>

          <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-8">
            {profile.allCollectibles.map((collectible, i) => {
              const rarity = collectible.rarity as keyof typeof rarityBorder
              const borderClass = rarityBorder[rarity] ?? 'border-stone-600'
              const glowColor = rarityGlowColor[rarity] ?? 'rgba(120,110,90,0.3)'

              return (
                <motion.div
                  key={collectible.id}
                  initial={{ opacity: 0, y: 40 }}
                  whileInView={{ opacity: 1, y: 0 }}
                  viewport={{ once: true }}
                  transition={{ delay: i * 0.1, duration: 0.7 }}
                  onClick={() => collectible.isEarned && setSelectedCollectible(collectible)}
                  className={`relative flex flex-col items-center text-center transition-all duration-500 ${
                    collectible.isEarned ? 'cursor-pointer group' : 'cursor-default opacity-40'
                  }`}
                >
                  {/* Pedestal base */}
                  <div className="relative w-full flex flex-col items-center">

                    {/* Glow platform */}
                    {collectible.isEarned && (
                      <motion.div
                        animate={{ opacity: [0.4, 0.8, 0.4] }}
                        transition={{ duration: 3, repeat: Infinity }}
                        className="absolute bottom-8 w-32 h-4 rounded-full blur-xl"
                        style={{ background: glowColor }}
                      />
                    )}

                    {/* Collectible image */}
                    <motion.div
                      whileHover={collectible.isEarned ? { y: -8, scale: 1.05 } : {}}
                      transition={{ duration: 0.3 }}
                      className={`relative w-40 h-40 rounded-full border-2 overflow-hidden mb-0 ${
                        collectible.isEarned ? borderClass : 'border-stone-800'
                      }`}
                      style={collectible.isEarned ? {
                        boxShadow: `0 0 30px ${glowColor}, 0 0 60px ${glowColor.replace('0.4', '0.2')}`
                      } : {}}
                    >
                      {collectible.isEarned ? (
                        <img
                          src={collectible.imageUrl}
                          alt={collectible.name}
                          className="w-full h-full object-cover"
                        />
                      ) : (
                        <div className="w-full h-full bg-stone-900 flex items-center justify-center">
                          <span className="text-stone-700 text-5xl font-bold">?</span>
                        </div>
                      )}
                    </motion.div>

                    {/* Pedestal stem */}
                    <div className="w-px h-8 bg-linear-to-b from-stone-600 to-transparent" />

                    {/* Pedestal base plate */}
                    <div className={`w-28 h-px ${collectible.isEarned ? 'bg-stone-500' : 'bg-stone-800'}`} />
                    <div className={`w-20 h-px mt-0.5 ${collectible.isEarned ? 'bg-stone-600' : 'bg-stone-800'}`} />
                  </div>

                  {/* Name and rarity */}
                  <div className="mt-4">
                    {collectible.isEarned ? (
                      <>
                        <p className="text-amber-50 text-lg font-bold mb-1 group-hover:text-amber-200 transition-colors">
                          {collectible.name}
                        </p>
                        <p className={`text-sm tracking-[0.3em] uppercase ${rarityText[rarity] ?? 'text-stone-400'}`}>
                          {collectible.rarity}
                        </p>
                        <p className="text-stone-600 text-xs mt-2">Click to inspect</p>
                      </>
                    ) : (
                      <>
                        <p className="text-stone-700 text-lg font-bold mb-1">???</p>
                        <p className="text-stone-800 text-sm tracking-[0.3em] uppercase">Locked</p>
                      </>
                    )}
                  </div>
                </motion.div>
              )
            })}
          </div>
        </section>

        {/* ── Era Progress ── */}
        <section>
          <motion.div
            initial={{ opacity: 0 }}
            whileInView={{ opacity: 1 }}
            viewport={{ once: true }}
            className="flex items-center gap-4 mb-4"
          >
            <p className="text-amber-200/50 text-sm tracking-[0.5em] uppercase">
              Era Progress
            </p>
            <div className="h-px flex-1 bg-stone-800" />
          </motion.div>

          <motion.p
            initial={{ opacity: 0 }}
            whileInView={{ opacity: 1 }}
            viewport={{ once: true }}
            className="text-stone-500 text-base mb-16"
          >
            Complete all chapters in an era to unlock the Grandmaster badge.
          </motion.p>

          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
            {profile.eraProgress.map((era, i) => {
              const accent = eraAccents[era.colorTheme] ?? 'text-stone-300'
              const image = eraImages[era.colorTheme]
              const percent = era.totalChapters > 0
                ? Math.round((era.completedChapters / era.totalChapters) * 100)
                : 0

              return (
                <motion.div
                  key={era.eraId}
                  initial={{ opacity: 0, y: 20 }}
                  whileInView={{ opacity: 1, y: 0 }}
                  viewport={{ once: true }}
                  transition={{ delay: i * 0.08 }}
                  onClick={() => navigate(`/eras/${era.eraId}`)}
                  className="relative overflow-hidden border border-stone-800 hover:border-stone-600 cursor-pointer transition-all duration-500 group h-40"
                >
                  {image && (
                    <>
                      <img
                        src={image}
                        alt={era.eraName}
                        className="absolute inset-0 w-full h-full object-cover opacity-20 group-hover:opacity-30 transition-opacity duration-500"
                        style={{ objectPosition: '50% 30%' }}
                      />
                      <div className="absolute inset-0 bg-linear-to-r from-stone-950/90 to-stone-950/60" />
                    </>
                  )}

                  <div className="relative z-10 p-6 h-full flex flex-col justify-between">
                    <div className="flex items-start justify-between">
                      <div>
                        <p className={`text-xs tracking-[0.3em] uppercase mb-2 ${accent}`}>
                          Era {i + 1}
                        </p>
                        <h3 className="text-stone-200 font-bold text-2xl group-hover:text-amber-50 transition-colors">
                          {era.eraName}
                        </h3>
                      </div>
                      {era.isGrandmasterUnlocked && (
                        <motion.p
                          animate={{ opacity: [0.7, 1, 0.7] }}
                          transition={{ duration: 2, repeat: Infinity }}
                          className="text-yellow-400 text-base"
                        >
                          ★ Grandmaster
                        </motion.p>
                      )}
                    </div>

                    <div>
                      <div className="flex items-center justify-between mb-2">
                        <p className="text-stone-500 text-sm">
                          {era.completedChapters}/{era.totalChapters} chapters
                        </p>
                        <p className={`text-base font-bold ${accent}`}>{percent}%</p>
                      </div>
                      <div className="h-px bg-stone-800 w-full">
                        <motion.div
                          initial={{ width: 0 }}
                          whileInView={{ width: `${percent}%` }}
                          viewport={{ once: true }}
                          transition={{ duration: 1.2, delay: i * 0.08 }}
                          className={`h-px ${accent.replace('text-', 'bg-')}`}
                        />
                      </div>
                    </div>
                  </div>
                </motion.div>
              )
            })}
          </div>
        </section>

      </div>

      {/* ── Collectible Fullscreen Inspect Modal ── */}
      <AnimatePresence>
        {selectedCollectible && (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            onClick={() => setSelectedCollectible(null)}
            className="fixed inset-0 z-50 flex items-center justify-center px-4 backdrop-blur-xl cursor-pointer"
            style={{
              background: 'radial-gradient(ellipse at center, rgba(180,120,30,0.1) 0%, rgba(0,0,0,0.97) 70%)'
            }}
          >
            <motion.div
              initial={{ scale: 0.8, opacity: 0, y: 40 }}
              animate={{ scale: 1, opacity: 1, y: 0 }}
              exit={{ scale: 0.8, opacity: 0, y: 40 }}
              transition={{ duration: 0.5, ease: [0.32, 0.72, 0, 1] }}
              onClick={e => e.stopPropagation()}
              className="flex flex-col items-center text-center max-w-md cursor-default"
            >
              {/* Glow */}
              <motion.div
                animate={{ opacity: [0.3, 0.7, 0.3], scale: [1, 1.1, 1] }}
                transition={{ duration: 3, repeat: Infinity }}
                className="absolute w-80 h-80 rounded-full blur-3xl"
                style={{
                  background: rarityGlowColor[selectedCollectible.rarity as keyof typeof rarityGlowColor]
                    ?? 'rgba(120,110,90,0.3)'
                }}
              />

              {/* Full image */}
              <motion.div
                initial={{ y: 20 }}
                animate={{ y: [0, -8, 0] }}
                transition={{ duration: 4, repeat: Infinity, ease: 'easeInOut' }}
                className={`relative w-64 h-64 rounded-full border-2 overflow-hidden mb-6 ${
                  rarityBorder[selectedCollectible.rarity as keyof typeof rarityBorder] ?? 'border-stone-500'
                }`}
                style={{
                  boxShadow: `0 0 60px ${rarityGlowColor[selectedCollectible.rarity as keyof typeof rarityGlowColor] ?? 'rgba(120,110,90,0.3)'}`
                }}
              >
                <img
                  src={selectedCollectible.imageUrl}
                  alt={selectedCollectible.name}
                  className="w-full h-full object-cover"
                />
              </motion.div>

              {/* Pedestal */}
              <div className="w-px h-10 bg-linear-to-b from-stone-500 to-transparent" />
              <div className="w-48 h-px bg-stone-500" />
              <div className="w-32 h-px mt-0.5 bg-stone-600" />

              {/* Info */}
              <div className="mt-8 bg-black/50 backdrop-blur-sm px-10 py-8 border border-stone-800">
                <p className="text-amber-50 text-3xl font-bold mb-2">
                  {selectedCollectible.name}
                </p>
                <p className={`text-sm tracking-[0.3em] uppercase mb-6 ${
                  rarityText[selectedCollectible.rarity as keyof typeof rarityText] ?? 'text-stone-400'
                }`}>
                  {selectedCollectible.rarity} Collectible
                </p>
                <p className="text-stone-300 text-base leading-relaxed mb-8">
                  {selectedCollectible.description}
                </p>
                <button
                  onClick={() => setSelectedCollectible(null)}
                  className="text-stone-500 text-sm tracking-widest uppercase hover:text-amber-200 transition-colors cursor-pointer"
                >
                  ✕ Close
                </button>
              </div>
            </motion.div>
          </motion.div>
        )}
      </AnimatePresence>
    </div>
  )
}