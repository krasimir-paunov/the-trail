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
  exploration: '/images/eras/ExplorationEra.png',
  modern: '/images/eras/ModernEra.png',
  digital: '/images/eras/DigitalEra.png',
}

const eraAccentColors: Record<string, string> = {
  prehistoric: '#b03a08',
  ancient: '#8a6a08',
  medieval: '#7a1515',
  renaissance: '#4a2a7a',
  exploration: '#0a5a3a',
  modern: '#3a3a3a',
  digital: '#0a3a7a',
}

const eraAccentLight: Record<string, string> = {
  prehistoric: '#fb923c',
  ancient: '#facc15',
  medieval: '#f87171',
  renaissance: '#d8b4fe',
  exploration: '#34d399',
  modern: '#d4d4d8',
  digital: '#60a5fa',
}

const eraDescriptions: Record<string, string> = {
  prehistoric: 'The dawn of life — from the first organisms to the rise of early man.',
  ancient: 'Empires of stone and sand — civilisations that shaped the world.',
  medieval: 'An age of castles, crusades, and the birth of nations.',
  renaissance: 'When art and reason rekindled the flame of human greatness.',
  exploration: 'When sailors crossed unknown oceans and two worlds collided forever.',
  modern: 'Revolution, industry, and the wars that forged the present.',
  digital: 'The age of code — humanity\'s latest and fastest frontier.',
}

const rarityGlowColor: Record<string, string> = {
  Common: 'rgba(180,150,90,0.6)',
  Rare: 'rgba(59,130,246,0.6)',
  Legendary: 'rgba(250,204,21,0.9)',
}
const rarityBorderColor: Record<string, string> = {
  Common: '#a08060',
  Rare: '#3b82f6',
  Legendary: '#facc15',
}
const rarityLabel: Record<string, string> = {
  Common: '#c4a47a',
  Rare: '#60a5fa',
  Legendary: '#facc15',
}
const rarityTextModal: Record<string, string> = {
  Common: 'text-stone-400',
  Rare: 'text-blue-400',
  Legendary: 'text-yellow-400',
}

const romanNumerals = ['I', 'II', 'III', 'IV', 'V', 'VI', 'VII']
const ERA_ORDER = ['prehistoric', 'ancient', 'medieval', 'renaissance', 'exploration', 'modern', 'digital']

const CARD_W = 580
const CARD_H = 170
const CARD_H_HOVER = Math.round(CARD_H * 1.55)
const CARD_W_HOVER = Math.round(CARD_W * 1.18)
const CONTAINER_W = 1080
const VERTICAL_STEP = 300

const ZIGZAG = Array.from({ length: 7 }, (_, i) => ({
  side: i % 2 === 0 ? 'left' : 'right' as 'left' | 'right',
  left: i % 2 === 0 ? 0 : CONTAINER_W - CARD_W,
  top: i * VERTICAL_STEP,
}))

const CONTAINER_H = VERTICAL_STEP * 6 + CARD_H + 60

const TRAIL_PTS = ZIGZAG.map(z => ({
  x: z.side === 'left' ? z.left + CARD_W : z.left,
  y: z.top + CARD_H / 2,
}))

function buildWindingPath(pts: { x: number; y: number }[]): string {
  let d = `M ${pts[0].x} ${pts[0].y}`
  for (let i = 1; i < pts.length; i++) {
    const p = pts[i - 1]
    const c = pts[i]
    const midY = (p.y + c.y) / 2
    const swing = CONTAINER_W * 0.4
    const dx = c.x - p.x
    const cp1x = p.x + (dx > 0 ? swing : -swing)
    const cp2x = c.x + (dx > 0 ? -swing : swing)
    d += ` C ${cp1x} ${midY - 40}, ${cp2x} ${midY + 40}, ${c.x} ${c.y}`
  }
  return d
}

export default function ProfilePage() {
  const { isAuthenticated } = useAuth()
  const navigate = useNavigate()
  const [profile, setProfile] = useState<ProfileDto | null>(null)
  const [loading, setLoading] = useState(true)
  const [selectedCollectible, setSelectedCollectible] = useState<CollectibleDto | null>(null)
  const [hoveredEra, setHoveredEra] = useState<number | null>(null)
  const [expandedEra, setExpandedEra] = useState<string | null>(null)

  useEffect(() => {
    if (!isAuthenticated) { navigate('/login'); return }
    apiClient.get<ProfileDto>('/api/profile')
      .then(res => { setProfile(res.data); setLoading(false) })
      .catch(() => navigate('/'))
  }, [isAuthenticated, navigate])

  if (loading || !profile) {
    return (
      <div className="min-h-screen bg-stone-950 flex items-center justify-center">
        <motion.div animate={{ opacity: [0.3, 1, 0.3] }} transition={{ duration: 2, repeat: Infinity }}
          style={{ fontFamily: 'Cinzel, serif', color: 'rgba(212,168,83,0.5)', fontSize: '1.2rem', letterSpacing: '0.3em' }}>
          LOADING YOUR TRAIL...
        </motion.div>
      </div>
    )
  }

  const trailPath = buildWindingPath(TRAIL_PTS)

  const stats = [
    { value: profile.chaptersRead, label: 'Chapters Read' },
    { value: profile.quizzesPassed, label: 'Quizzes Passed' },
    { value: profile.earnedCollectibles.length, label: 'Collectibles' },
    { value: profile.eraProgress.filter(e => e.isGrandmasterUnlocked).length, label: 'Eras Mastered' },
  ]

  // ── Build era groups ──────────────────────────────────────────────
  const chapterCollectibles = profile.allCollectibles.filter(c => c.rarity !== 'Legendary')
  const legendaryCollectibles = profile.allCollectibles.filter(c => c.rarity === 'Legendary')

  const eraGroups = profile.eraProgress.map((era, eraIndex) => {
    const eraThemeIndex = ERA_ORDER.indexOf(era.colorTheme)
    const startIndex = (eraThemeIndex >= 0 ? eraThemeIndex : eraIndex) * 10
    const eraChapterCollectibles = chapterCollectibles.slice(startIndex, startIndex + 10)
    const eraLegendary = legendaryCollectibles[eraIndex] ?? null
    const allInEra = [...eraChapterCollectibles, ...(eraLegendary ? [eraLegendary] : [])]

    return {
      era,
      chapterCollectibles: eraChapterCollectibles,
      legendary: eraLegendary,
      allCollectibles: allInEra,
      earnedCount: allInEra.filter(c => c.isEarned).length,
      totalCount: allInEra.length,
    }
  })

  const renderCollectible = (collectible: CollectibleDto, i: number) => {
    const rarity = collectible.rarity
    const glowColor = rarityGlowColor[rarity] ?? 'rgba(180,150,90,0.5)'
    const borderColor = rarityBorderColor[rarity] ?? '#a08060'
    const labelColor = rarityLabel[rarity] ?? '#c4a47a'
    const isLegendary = rarity === 'Legendary'

    return (
      <motion.div
        key={collectible.id}
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ delay: i * 0.04, duration: 0.4 }}
        className="flex flex-col items-center"
        style={{ opacity: collectible.isEarned ? 1 : 0.28 }}
      >
        {/* Orb area */}
        <div
          className="w-full flex flex-col items-center pt-6 pb-2 px-1 relative"
          style={{ minHeight: isLegendary ? '175px' : '155px' }}
        >
          {collectible.isEarned && (
            <>
              <motion.div
                animate={{ opacity: [0.5, 1, 0.5], scale: [0.9, 1.05, 0.9] }}
                transition={{ duration: 3 + i * 0.3, repeat: Infinity }}
                className="absolute bottom-3 rounded-full blur-2xl"
                style={{
                  width: isLegendary ? '120px' : '80px',
                  height: isLegendary ? '28px' : '20px',
                  background: glowColor,
                }}
              />
              {isLegendary && (
                <motion.div
                  animate={{ opacity: [0.2, 0.5, 0.2], scale: [1, 1.08, 1] }}
                  transition={{ duration: 4, repeat: Infinity }}
                  className="absolute rounded-full blur-3xl"
                  style={{ width: '140px', height: '140px', top: '6px', background: 'rgba(250,204,21,0.15)' }}
                />
              )}
            </>
          )}

          {isLegendary && collectible.isEarned && (
            <motion.div
              animate={{ opacity: [0.7, 1, 0.7] }}
              transition={{ duration: 2, repeat: Infinity }}
              className="absolute top-1"
            >
              <span style={{ color: '#facc15', fontSize: '0.8rem' }}>★</span>
            </motion.div>
          )}

          <motion.div
            whileHover={collectible.isEarned ? { y: -7, scale: 1.06 } : {}}
            transition={{ duration: 0.35, ease: 'easeOut' }}
            onClick={() => collectible.isEarned && setSelectedCollectible(collectible)}
            className="relative rounded-full overflow-hidden"
            style={{
              cursor: collectible.isEarned ? 'pointer' : 'default',
              width: isLegendary ? '100px' : '86px',
              height: isLegendary ? '100px' : '86px',
              borderStyle: 'solid',
              borderWidth: isLegendary ? '3px' : '2px',
              borderColor: collectible.isEarned ? borderColor : 'rgba(100,80,50,0.2)',
              boxShadow: collectible.isEarned
                ? isLegendary
                  ? `0 0 28px ${glowColor}, 0 0 56px ${glowColor.replace('0.9', '0.3')}, 0 0 80px ${glowColor.replace('0.9', '0.1')}, inset 0 0 12px rgba(0,0,0,0.4)`
                  : `0 0 16px ${glowColor}, 0 0 36px ${glowColor.replace('0.6', '0.15')}, inset 0 0 12px rgba(0,0,0,0.4)`
                : 'none',
            }}
          >
            {collectible.isEarned ? (
              <img src={collectible.imageUrl} alt={collectible.name} className="w-full h-full object-cover" />
            ) : (
              <div className="w-full h-full flex items-center justify-center" style={{ background: '#1a1208' }}>
                <span style={{
                  color: 'rgba(100,80,50,0.3)',
                  fontFamily: 'Cinzel, serif',
                  fontSize: isLegendary ? '1.2rem' : '1rem',
                  fontWeight: 700,
                }}>?</span>
              </div>
            )}
          </motion.div>

          <div className="w-px flex-1 mt-2" style={{
            background: `linear-gradient(to bottom, ${collectible.isEarned ? borderColor : 'rgba(196,164,122,0.2)'}, transparent)`,
            minHeight: '12px',
          }} />
        </div>

        {/* Shelf */}
        <div className="w-full relative" style={{ height: '9px' }}>
          <div className="absolute inset-0" style={{
            background: isLegendary ? 'linear-gradient(to bottom, #6b4a18, #3d2208)' : 'linear-gradient(to bottom, #5a3818, #2d1a08)',
            borderTop: `1px solid ${isLegendary ? '#9a6a28' : '#7a4f20'}`,
            boxShadow: isLegendary
              ? '0 3px 14px rgba(250,204,21,0.12), 0 3px 10px rgba(0,0,0,0.7)'
              : '0 3px 10px rgba(0,0,0,0.7)',
          }} />
          <div className="absolute left-0 right-0 top-full h-5"
            style={{ background: 'linear-gradient(to bottom, rgba(0,0,0,0.45), transparent)' }} />
        </div>

        {/* Label — Fix 1: bumped font sizes */}
        <div className="pt-3 pb-5 text-center px-1">
          {collectible.isEarned ? (
            <>
              {isLegendary && (
                <p style={{
                  color: 'rgba(250,204,21,0.5)', fontFamily: 'Cinzel, serif',
                  fontSize: '0.65rem', letterSpacing: '0.25em',
                  textTransform: 'uppercase', marginBottom: '2px',
                }}>★ Grandmaster</p>
              )}
              <p style={{
                color: isLegendary ? '#fde68a' : '#e8d5a8',
                fontFamily: 'Cinzel, serif',
                fontSize: '0.72rem',
                fontWeight: 700,
                lineHeight: 1.3,
                marginBottom: '2px',
              }}>{collectible.name}</p>
              <p style={{
                color: labelColor, fontFamily: 'Cinzel, serif',
                fontSize: '0.65rem', letterSpacing: '0.22em', textTransform: 'uppercase',
              }}>{collectible.rarity}</p>
            </>
          ) : (
            <>
              {isLegendary && (
                <p style={{
                  color: 'rgba(250,204,21,0.5)', fontFamily: 'Cinzel, serif',
                  fontSize: '0.65rem', letterSpacing: '0.22em',
                  textTransform: 'uppercase', marginBottom: '2px',
                }}>★ Era Mastery</p>
              )}
              <p style={{
                color: 'rgba(196,164,122,0.55)', fontFamily: 'Cinzel, serif',
                fontSize: '0.72rem', fontWeight: 700,
              }}>???</p>
              <p style={{
                color: 'rgba(196,164,122,0.4)', fontFamily: 'Cinzel, serif',
                fontSize: '0.65rem', letterSpacing: '0.22em', textTransform: 'uppercase',
              }}>{isLegendary ? 'Master Era' : 'Locked'}</p>
            </>
          )}
        </div>
      </motion.div>
    )
  }

  return (
    <div className="min-h-screen" style={{ backgroundColor: '#1e1105' }}>

      {/* Fixed bg */}
      <div className="fixed inset-0 pointer-events-none" style={{ zIndex: 0 }}>
        <img src="/images/profile-background.png" alt="" className="w-full h-full object-cover"
          style={{ opacity: 0.22 }} />
        <div className="absolute inset-0" style={{
          background: 'radial-gradient(ellipse at 50% 30%, rgba(35,18,4,0.55) 0%, rgba(8,4,1,0.78) 100%)'
        }} />
      </div>

      <div className="relative" style={{ zIndex: 1 }}>

        {/* ══ HERO ══ */}
        <div className="flex flex-col items-center text-center px-8 pt-20 pb-24">
          <motion.h1 initial={{ opacity: 0, y: -16 }} animate={{ opacity: 1, y: 0 }} transition={{ duration: 0.9 }}
            style={{
              fontFamily: 'Cinzel, serif', fontSize: 'clamp(2.8rem, 6vw, 5rem)', fontWeight: 700,
              color: '#d4a853', letterSpacing: '0.16em', textTransform: 'uppercase',
              textShadow: '0 0 50px rgba(212,168,83,0.4), 0 2px 4px rgba(0,0,0,0.9)', marginBottom: '0.6rem',
            }}>Explorer Profile</motion.h1>

          <motion.div initial={{ scaleX: 0, opacity: 0 }} animate={{ scaleX: 1, opacity: 1 }}
            transition={{ delay: 0.3, duration: 0.8 }}
            className="flex items-center gap-3 mb-6" style={{ width: 'min(480px, 80vw)' }}>
            <div className="h-px flex-1" style={{ backgroundColor: 'rgba(212,168,83,0.35)' }} />
            <span style={{ color: 'rgba(212,168,83,0.7)', fontFamily: 'EB Garamond, serif', fontSize: '1.3rem' }}>✦</span>
            <div className="h-px flex-1" style={{ backgroundColor: 'rgba(212,168,83,0.35)' }} />
          </motion.div>

          <motion.p initial={{ opacity: 0, y: 12 }} animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.15, duration: 0.9 }}
            style={{
              fontFamily: 'Cinzel, serif', fontSize: 'clamp(3.5rem, 10vw, 7.5rem)', fontWeight: 700,
              color: '#f5e8c8', textShadow: '0 2px 40px rgba(0,0,0,0.95)', lineHeight: 1, marginBottom: '3.5rem',
            }}>{profile.displayName}</motion.p>

          <motion.div initial={{ opacity: 0, y: 10 }} animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.4, duration: 0.7 }} className="flex items-stretch">
            {stats.map((stat, i) => (
              <div key={i} className="flex items-stretch">
                <div className="flex flex-col items-center px-10">
                  <span style={{
                    fontFamily: 'Cinzel, serif', fontSize: 'clamp(2.5rem, 5vw, 3.8rem)',
                    fontWeight: 700, color: '#d4a853', lineHeight: 1, textShadow: '0 0 25px rgba(212,168,83,0.45)',
                  }}>{stat.value}</span>
                  <span style={{
                    fontFamily: 'Cinzel, serif', fontSize: '0.68rem', letterSpacing: '0.28em',
                    color: 'rgba(212,168,83,0.5)', textTransform: 'uppercase', marginTop: '0.5rem',
                  }}>{stat.label}</span>
                </div>
                {i < stats.length - 1 && (
                  <div style={{ width: '1px', backgroundColor: 'rgba(212,168,83,0.2)', margin: '4px 0' }} />
                )}
              </div>
            ))}
          </motion.div>
        </div>

        {/* ══ TROPHY CABINET — Accordion ══ */}
        <div className="max-w-6xl mx-auto px-8 pb-20">
          <motion.div initial={{ opacity: 0 }} whileInView={{ opacity: 1 }} viewport={{ once: true }}
            className="flex items-center gap-4 mb-3">
            <div className="h-px flex-1" style={{ backgroundColor: 'rgba(196,164,122,0.18)' }} />
            <p style={{ color: '#c4a47a', fontFamily: 'Cinzel, serif', fontSize: '0.75rem', letterSpacing: '0.5em' }}>TROPHY CABINET</p>
            <div className="h-px flex-1" style={{ backgroundColor: 'rgba(196,164,122,0.18)' }} />
            <p style={{ color: 'rgba(196,164,122,0.45)', fontFamily: 'Cinzel, serif', fontSize: '0.85rem' }}>
              {profile.earnedCollectibles.length}<span style={{ color: 'rgba(196,164,122,0.22)' }}>/{profile.allCollectibles.length}</span>
            </p>
          </motion.div>
          <motion.p initial={{ opacity: 0 }} whileInView={{ opacity: 1 }} viewport={{ once: true }}
            className="text-center mb-10"
            style={{ color: 'rgba(196,164,122,0.45)', fontFamily: 'EB Garamond, serif', fontSize: '1.1rem', fontStyle: 'italic' }}>
            Complete chapters and pass quizzes to expand your collection.
          </motion.p>

          {/* Era accordion rows */}
          <div className="flex flex-col gap-2">
            {eraGroups.map((group, groupIndex) => {
              const accentLight = eraAccentLight[group.era.colorTheme] ?? '#d4a853'
              const accentDark = eraAccentColors[group.era.colorTheme] ?? '#6b3a10'
              const isExpanded = expandedEra === group.era.colorTheme

              return (
                <motion.div
                  key={group.era.eraId}
                  initial={{ opacity: 0, y: 16 }}
                  whileInView={{ opacity: 1, y: 0 }}
                  viewport={{ once: true }}
                  transition={{ delay: groupIndex * 0.07, duration: 0.5 }}
                >
                  {/* Row header */}
                  <motion.div
                    onClick={() => setExpandedEra(isExpanded ? null : group.era.colorTheme)}
                    className="flex items-center justify-between px-6 py-4 cursor-pointer transition-all duration-300"
                    style={{
                      background: isExpanded ? 'rgba(30,15,4,0.97)' : 'rgba(20,10,2,0.8)',
                      border: `1px solid ${isExpanded ? accentLight + '55' : 'rgba(100,65,20,0.3)'}`,
                      borderLeft: `3px solid ${accentLight}`,
                      borderBottom: isExpanded ? 'none' : `1px solid ${isExpanded ? accentLight + '55' : 'rgba(100,65,20,0.3)'}`,
                    }}
                    whileHover={{ backgroundColor: 'rgba(28,13,3,0.97)' }}
                  >
                    <div className="flex items-center gap-4">
                      <span style={{
                        fontFamily: 'Cinzel, serif', fontSize: '0.62rem',
                        letterSpacing: '0.3em', color: accentLight, opacity: 0.65,
                        textTransform: 'uppercase', minWidth: '38px',
                      }}>
                        Era {romanNumerals[groupIndex]}
                      </span>
                      <span style={{
                        fontFamily: 'Cinzel, serif', fontSize: '1.05rem',
                        fontWeight: 700, color: '#f5e8c8',
                      }}>
                        {group.era.eraName}
                      </span>
                      {group.era.isGrandmasterUnlocked && (
                        <motion.span
                          animate={{ opacity: [0.6, 1, 0.6] }}
                          transition={{ duration: 2, repeat: Infinity }}
                          style={{ fontFamily: 'Cinzel, serif', fontSize: '0.62rem', color: '#facc15', letterSpacing: '0.08em' }}
                        >
                          ★ Grandmaster
                        </motion.span>
                      )}
                    </div>

                    <div className="flex items-center gap-5">
                      <div className="hidden md:flex items-center gap-3">
                        <div style={{ width: '110px', height: '2px', background: 'rgba(196,164,122,0.12)', borderRadius: '1px' }}>
                          <motion.div
                            initial={{ width: 0 }}
                            whileInView={{ width: `${group.totalCount > 0 ? (group.earnedCount / group.totalCount) * 100 : 0}%` }}
                            viewport={{ once: true }}
                            transition={{ duration: 1, delay: groupIndex * 0.07 }}
                            style={{ height: '2px', background: accentLight, borderRadius: '1px' }}
                          />
                        </div>
                        <span style={{ fontFamily: 'Cinzel, serif', fontSize: '0.68rem', color: accentLight, minWidth: '36px' }}>
                          {group.earnedCount}/{group.totalCount}
                        </span>
                      </div>

                      <motion.span
                        animate={{ rotate: isExpanded ? -90 : 90 }}
                        transition={{ duration: 0.3, ease: 'easeInOut' }}
                        style={{ color: accentLight, fontSize: '1rem', display: 'block', lineHeight: 1, opacity: 0.8 }}
                      >
                        ›
                      </motion.span>
                    </div>
                  </motion.div>

                  {/* Expanded shelf */}
                  <AnimatePresence>
                    {isExpanded && (
                      <motion.div
                        initial={{ height: 0, opacity: 0 }}
                        animate={{ height: 'auto', opacity: 1 }}
                        exit={{ height: 0, opacity: 0 }}
                        transition={{ duration: 0.4, ease: [0.32, 0.72, 0, 1] }}
                        style={{ overflow: 'hidden' }}
                      >
                        <div style={{
                          background: 'rgba(11,6,1,0.98)',
                          border: `1px solid ${accentLight}40`,
                          borderLeft: `3px solid ${accentLight}`,
                          borderTop: `1px solid ${accentDark}60`,
                          padding: '20px 20px 4px',
                        }}>
                          <div style={{
                            height: '1px',
                            background: `linear-gradient(to right, ${accentLight}50, transparent)`,
                            marginBottom: '12px',
                          }} />

                          {/* 10 chapter collectibles in a row */}
                          <div style={{ display: 'grid', gridTemplateColumns: 'repeat(10, 1fr)', gap: '4px' }}>
                            {group.chapterCollectibles.map((c, i) => renderCollectible(c, i))}
                          </div>

                          {/* Fix 2: Legendary centered below */}
                          {group.legendary && (
                            <div style={{
                              marginTop: '8px',
                              paddingTop: '16px',
                              borderTop: `1px solid rgba(250,204,21,0.12)`,
                              display: 'flex',
                              flexDirection: 'column',
                              alignItems: 'center',
                              textAlign: 'center',
                            }}>
                              <p style={{
                                fontFamily: 'Cinzel, serif', fontSize: '0.72rem',
                                letterSpacing: '0.4em', color: 'rgba(250,204,21,0.7)',
                                textTransform: 'uppercase', marginBottom: '12px',
                              }}>★ Era Grandmaster Reward</p>
                              <div style={{ width: '140px' }}>
                                {renderCollectible(group.legendary, 10)}
                              </div>
                              <p style={{
                                fontFamily: 'EB Garamond, serif', fontSize: '1rem',
                                color: group.legendary.isEarned ? 'rgba(196,164,122,0.55)' : 'rgba(196,164,122,0.8)',
                                fontStyle: 'italic', lineHeight: 1.5, maxWidth: '520px',
                                marginTop: '4px',
                              }}>
                                {group.legendary.isEarned
                                  ? group.legendary.description
                                  : `Complete all chapters and quizzes in ${group.era.eraName} to unlock this legendary reward.`}
                              </p>
                            </div>
                          )}

                          <div style={{ height: '12px' }} />
                        </div>
                      </motion.div>
                    )}
                  </AnimatePresence>
                </motion.div>
              )
            })}
          </div>
        </div>

        {/* ══ ERA PROGRESS — Atlas ══ */}
        <div className="px-4 pb-32">
          <div style={{ maxWidth: '1300px', margin: '0 auto' }}>
            <motion.div
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              style={{
                background: 'linear-gradient(160deg, #f5ecd0 0%, #eee0b0 25%, #f5ecd0 50%, #eee0b0 75%, #e8d5a0 100%)',
                boxShadow: '0 40px 100px rgba(0,0,0,0.9), 0 0 0 2px rgba(80,42,6,0.6)',
                position: 'relative',
              }}
            >
              <div className="absolute inset-0 pointer-events-none" style={{
                backgroundImage: `repeating-linear-gradient(0deg, transparent, transparent 32px, rgba(70,32,4,0.07) 32px, rgba(70,32,4,0.07) 33px)`,
                zIndex: 1,
              }} />
              <div className="absolute inset-0 pointer-events-none" style={{ zIndex: 1 }}>
                <img src="/images/profile-background.png" alt="" className="w-full h-full object-cover"
                  style={{ opacity: 0.08, mixBlendMode: 'multiply' }} />
              </div>
              <div className="absolute inset-0 pointer-events-none" style={{ border: '3px solid rgba(70,36,5,0.55)', zIndex: 6 }} />
              <div className="absolute pointer-events-none" style={{ inset: '10px', border: '1px solid rgba(70,36,5,0.25)', zIndex: 6 }} />
              <div className="absolute inset-y-0 pointer-events-none" style={{
                left: '50%', width: '3px', transform: 'translateX(-50%)',
                background: 'linear-gradient(to right, transparent, rgba(70,36,5,0.18) 50%, transparent)',
                zIndex: 4,
              }} />

              <div className="relative px-14 pt-12" style={{ zIndex: 5 }}>
                <div className="text-center mb-16">
                  <p style={{
                    fontFamily: 'Cinzel, serif', fontSize: '0.8rem', letterSpacing: '0.55em',
                    color: 'rgba(30,12,0,0.7)', textTransform: 'uppercase', marginBottom: '0.6rem',
                  }}>Cartography of Progress · Personal Record</p>
                  <h2 style={{
                    fontFamily: 'Cinzel, serif', fontSize: 'clamp(2rem, 3.2vw, 2.8rem)',
                    fontWeight: 700, color: '#140600', letterSpacing: '0.04em', marginBottom: '0.5rem',
                  }}>The Eras of History</h2>
                  <div className="flex items-center justify-center gap-3 mb-4">
                    <div className="h-px w-24" style={{ backgroundColor: 'rgba(30,12,0,0.35)' }} />
                    <span style={{ color: 'rgba(30,12,0,0.45)', fontSize: '1.1rem' }}>✦</span>
                    <div className="h-px w-24" style={{ backgroundColor: 'rgba(30,12,0,0.35)' }} />
                  </div>
                  <p style={{
                    fontFamily: 'EB Garamond, serif', fontSize: '1.2rem',
                    color: 'rgba(30,12,0,0.65)', fontStyle: 'italic',
                  }}>Each era a territory — chart your journey through the ages.</p>
                </div>

                <div className="relative mx-auto" style={{ width: `${CONTAINER_W}px`, height: `${CONTAINER_H}px` }}>
                  <svg width={CONTAINER_W} height={CONTAINER_H}
                    viewBox={`0 0 ${CONTAINER_W} ${CONTAINER_H}`}
                    className="absolute inset-0 pointer-events-none" style={{ zIndex: 1 }}>
                    <path d={trailPath} fill="none" stroke="rgba(120,65,8,0.12)" strokeWidth="18" strokeLinecap="round" />
                    <path d={trailPath} fill="none" stroke="rgba(100,50,5,0.18)" strokeWidth="8" strokeLinecap="round" />
                    <path d={trailPath} fill="none" stroke="rgba(55,25,3,0.55)"
                      strokeWidth="2.5" strokeDasharray="14 9" strokeLinecap="round" />
                    {TRAIL_PTS.map((pt, i) => (
                      <g key={i}>
                        <circle cx={pt.x} cy={pt.y} r="20"
                          fill="rgba(180,130,40,0.1)" stroke="rgba(55,25,3,0.2)" strokeWidth="1" />
                        <circle cx={pt.x} cy={pt.y} r="5.5" fill="rgba(55,25,3,0.5)" />
                        <text
                          x={ZIGZAG[i].side === 'left' ? pt.x - 34 : pt.x + 34}
                          y={pt.y + 4}
                          textAnchor={ZIGZAG[i].side === 'left' ? 'end' : 'start'}
                          fontSize="12" fill="rgba(35,14,2,0.55)"
                          fontFamily="Cinzel, serif" fontWeight="700">
                          {romanNumerals[i]}
                        </text>
                      </g>
                    ))}
                    <g transform={`translate(${CONTAINER_W / 2}, ${CONTAINER_H - 35})`} opacity="0.28">
                      <circle cx="0" cy="0" r="22" fill="none" stroke="rgba(40,18,2,0.9)" strokeWidth="0.9" />
                      <circle cx="0" cy="0" r="4" fill="rgba(40,18,2,0.6)" />
                      {[0, 45, 90, 135].map(deg => (
                        <line key={deg}
                          x1={Math.cos((deg - 90) * Math.PI / 180) * 6}
                          y1={Math.sin((deg - 90) * Math.PI / 180) * 6}
                          x2={Math.cos((deg - 90) * Math.PI / 180) * 22}
                          y2={Math.sin((deg - 90) * Math.PI / 180) * 22}
                          stroke="rgba(40,18,2,0.75)" strokeWidth={deg % 90 === 0 ? 1.4 : 0.7} />
                      ))}
                      <text x="0" y="-27" textAnchor="middle" fontSize="9"
                        fill="rgba(40,18,2,0.85)" fontFamily="Cinzel, serif" fontWeight="600">N</text>
                    </g>
                  </svg>

                  {profile.eraProgress.map((era, i) => {
                    const image = eraImages[era.colorTheme]
                    const accent = eraAccentColors[era.colorTheme] ?? '#6b3a10'
                    const accentLight = eraAccentLight[era.colorTheme] ?? '#d4a853'
                    const description = eraDescriptions[era.colorTheme] ?? ''
                    const percent = era.totalChapters > 0
                      ? Math.round((era.completedChapters / era.totalChapters) * 100) : 0
                    const pos = ZIGZAG[i]
                    const isHovered = hoveredEra === i
                    const hoverExtraW = CARD_W_HOVER - CARD_W
                    const hoverExtraH = CARD_H_HOVER - CARD_H
                    const hoverLeft = pos.side === 'right' ? pos.left - hoverExtraW : pos.left

                    return (
                      <motion.div
                        key={era.eraId}
                        initial={{ opacity: 0, y: 16 }}
                        whileInView={{ opacity: 1, y: 0 }}
                        viewport={{ once: true }}
                        transition={{ delay: i * 0.13, duration: 0.65 }}
                        animate={{
                          width: isHovered ? CARD_W_HOVER : CARD_W,
                          height: isHovered ? CARD_H_HOVER : CARD_H,
                          left: isHovered ? hoverLeft : pos.left,
                          top: isHovered ? pos.top - hoverExtraH / 2 : pos.top,
                          zIndex: isHovered ? 20 : 2 + i,
                        }}
                        // @ts-ignore
                        transition={{ duration: 0.4, ease: [0.32, 0.72, 0, 1] }}
                        onClick={() => navigate(`/eras/${era.eraId}`)}
                        onMouseEnter={() => setHoveredEra(i)}
                        onMouseLeave={() => setHoveredEra(null)}
                        className="absolute cursor-pointer overflow-hidden"
                        style={{
                          borderRadius: '2px',
                          border: isHovered ? `1px solid ${accentLight}aa` : '1px solid rgba(45,20,3,0.45)',
                          boxShadow: isHovered
                            ? `0 24px 70px rgba(0,0,0,0.55), 0 0 0 1px ${accentLight}60, 0 0 50px ${accentLight}18`
                            : '0 6px 28px rgba(0,0,0,0.32), 0 1px 0 rgba(190,150,50,0.25)',
                          display: 'flex',
                        }}
                      >
                        <motion.div
                          animate={{ width: isHovered ? 290 : 240 }}
                          transition={{ duration: 0.4, ease: [0.32, 0.72, 0, 1] }}
                          className="flex-shrink-0 flex flex-col justify-between relative overflow-hidden"
                          style={{
                            padding: '16px 18px',
                            background: 'linear-gradient(135deg, #dfc070 0%, #d4b568 100%)',
                            borderRight: `4px solid ${accent}`,
                          }}
                        >
                          <div className="absolute inset-0" style={{
                            background: `linear-gradient(135deg, ${accent}20 0%, transparent 65%)`,
                          }} />
                          <div className="relative">
                            <div className="flex items-center justify-between mb-1">
                              <span style={{
                                fontFamily: 'Cinzel, serif', fontSize: '0.62rem',
                                letterSpacing: '0.3em', color: `${accent}dd`, textTransform: 'uppercase',
                              }}>Era {romanNumerals[i]}</span>
                              {era.isGrandmasterUnlocked && (
                                <motion.span animate={{ opacity: [0.6, 1, 0.6] }} transition={{ duration: 2, repeat: Infinity }}
                                  style={{ fontFamily: 'Cinzel, serif', fontSize: '0.57rem', color: '#6a4e00', letterSpacing: '0.06em' }}>
                                  ★ Grandmaster
                                </motion.span>
                              )}
                            </div>
                            <h3 style={{
                              fontFamily: 'Cinzel, serif', fontSize: isHovered ? '1.4rem' : '1.15rem',
                              fontWeight: 700, color: '#120600', lineHeight: 1.2, marginBottom: '0.4rem',
                            }}>{era.eraName}</h3>
                            <p style={{
                              fontFamily: 'EB Garamond, serif', fontSize: isHovered ? '0.95rem' : '0.82rem',
                              color: 'rgba(18,6,0,0.68)', fontStyle: 'italic', lineHeight: 1.45,
                            }}>{description}</p>
                          </div>
                          <div className="relative">
                            <div className="flex justify-between mb-1">
                              <span style={{ fontFamily: 'EB Garamond, serif', fontSize: '0.75rem', color: 'rgba(18,6,0,0.55)', fontStyle: 'italic' }}>
                                {era.completedChapters}/{era.totalChapters} explored
                              </span>
                              <span style={{ fontFamily: 'Cinzel, serif', fontSize: '0.72rem', fontWeight: 700, color: accent }}>
                                {percent}%
                              </span>
                            </div>
                            <div style={{ height: '2px', backgroundColor: 'rgba(18,6,0,0.14)', borderRadius: '1px' }}>
                              <motion.div initial={{ width: 0 }} whileInView={{ width: `${percent}%` }}
                                viewport={{ once: true }} transition={{ duration: 1.2, delay: i * 0.13 }}
                                style={{ height: '2px', backgroundColor: accent, borderRadius: '1px' }} />
                            </div>
                          </div>
                        </motion.div>

                        <div className="flex-1 relative overflow-hidden">
                          <motion.img src={image} alt={era.eraName}
                            className="absolute inset-0 w-full h-full object-cover"
                            style={{ objectPosition: '50% 30%' }}
                            animate={{
                              scale: isHovered ? 1.08 : 1,
                              filter: isHovered ? 'brightness(1.35) contrast(1.05)' : 'brightness(0.85)',
                            }}
                            transition={{ duration: 0.5, ease: 'easeOut' }} />
                          <div className="absolute inset-0" style={{
                            background: 'linear-gradient(to right, rgba(200,168,80,0.3) 0%, transparent 30%)',
                          }} />
                          <motion.div animate={{ opacity: isHovered ? 1 : 0 }} transition={{ duration: 0.25 }}
                            className="absolute inset-0 flex items-center justify-center"
                            style={{ background: 'rgba(0,0,0,0.15)' }}>
                            <span style={{
                              fontFamily: 'Cinzel, serif', fontSize: '1rem', color: '#f5e8c8',
                              letterSpacing: '0.35em', textTransform: 'uppercase',
                              textShadow: '0 1px 10px rgba(0,0,0,0.9)',
                            }}>Explore →</span>
                          </motion.div>
                        </div>
                      </motion.div>
                    )
                  })}
                </div>

                <div style={{ height: '48px' }} />
              </div>
            </motion.div>

            {/* Footer */}
            <div style={{
              background: 'linear-gradient(to bottom, #1a0d04, #120800)',
              border: '1px solid rgba(120,70,15,0.35)',
              borderTop: '3px solid rgba(120,70,15,0.5)',
              padding: '28px 48px',
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'space-between',
              boxShadow: '0 20px 60px rgba(0,0,0,0.7)',
            }}>
              <div>
                <p style={{
                  fontFamily: 'Cinzel, serif', fontSize: '0.72rem',
                  letterSpacing: '0.45em', color: 'rgba(212,168,83,0.5)',
                  textTransform: 'uppercase', marginBottom: '0.35rem',
                }}>The Trail</p>
                <p style={{
                  fontFamily: 'EB Garamond, serif', fontSize: '1.05rem',
                  color: 'rgba(212,168,83,0.75)', fontStyle: 'italic',
                }}>A Chronicle of Human History</p>
              </div>
              <div className="flex items-center gap-3 flex-1 mx-12">
                <div className="h-px flex-1" style={{ backgroundColor: 'rgba(212,168,83,0.18)' }} />
                <span style={{ color: 'rgba(212,168,83,0.3)', fontSize: '0.9rem' }}>✦</span>
                <div className="h-px flex-1" style={{ backgroundColor: 'rgba(212,168,83,0.18)' }} />
              </div>
              <div className="text-right">
                <p style={{
                  fontFamily: 'Cinzel, serif', fontSize: '0.68rem',
                  letterSpacing: '0.4em', color: 'rgba(212,168,83,0.45)',
                  textTransform: 'uppercase', marginBottom: '0.35rem',
                }}>Progress</p>
                <p style={{
                  fontFamily: 'Cinzel, serif', fontSize: '1.3rem',
                  fontWeight: 700, color: '#d4a853',
                  textShadow: '0 0 20px rgba(212,168,83,0.3)',
                }}>
                  {profile.eraProgress.filter(e => e.isGrandmasterUnlocked).length}
                  <span style={{ color: 'rgba(212,168,83,0.35)', fontSize: '1rem' }}>
                    {' '}/ {profile.eraProgress.length}
                  </span>
                  <span style={{
                    fontFamily: 'Cinzel, serif', fontSize: '0.7rem',
                    letterSpacing: '0.25em', color: 'rgba(212,168,83,0.55)',
                    display: 'block', fontWeight: 400, marginTop: '2px',
                  }}>ERAS MASTERED</span>
                </p>
              </div>
            </div>
          </div>
        </div>

      </div>

      {/* ══ Inspect Modal ══ */}
      <AnimatePresence>
        {selectedCollectible && (
          <motion.div
            initial={{ opacity: 0 }} animate={{ opacity: 1 }} exit={{ opacity: 0 }}
            onClick={() => setSelectedCollectible(null)}
            className="fixed inset-0 z-50 flex items-center justify-center px-4 backdrop-blur-xl cursor-pointer"
            style={{
              background: selectedCollectible.rarity === 'Legendary'
                ? 'radial-gradient(ellipse at center, rgba(250,204,21,0.12) 0%, rgba(0,0,0,0.97) 65%)'
                : 'radial-gradient(ellipse at center, rgba(180,120,30,0.1) 0%, rgba(0,0,0,0.97) 70%)'
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
              <motion.div
                animate={{ opacity: [0.3, 0.7, 0.3], scale: [1, 1.1, 1] }}
                transition={{ duration: selectedCollectible.rarity === 'Legendary' ? 2 : 3, repeat: Infinity }}
                className="absolute rounded-full blur-3xl"
                style={{
                  width: selectedCollectible.rarity === 'Legendary' ? '420px' : '320px',
                  height: selectedCollectible.rarity === 'Legendary' ? '420px' : '320px',
                  background: rarityGlowColor[selectedCollectible.rarity] ?? 'rgba(120,110,90,0.3)',
                }}
              />
              {selectedCollectible.rarity === 'Legendary' && (
                <motion.div
                  animate={{ opacity: [0.1, 0.3, 0.1], scale: [0.9, 1.2, 0.9] }}
                  transition={{ duration: 3, repeat: Infinity }}
                  className="absolute rounded-full blur-3xl"
                  style={{ width: '600px', height: '600px', background: 'rgba(250,204,21,0.08)' }}
                />
              )}
              <motion.div
                initial={{ y: 20 }}
                animate={{ y: [0, -8, 0] }}
                transition={{ duration: selectedCollectible.rarity === 'Legendary' ? 3 : 4, repeat: Infinity, ease: 'easeInOut' }}
                className="relative rounded-full overflow-hidden mb-6"
                style={{
                  width: selectedCollectible.rarity === 'Legendary' ? '280px' : '256px',
                  height: selectedCollectible.rarity === 'Legendary' ? '280px' : '256px',
                  borderStyle: 'solid',
                  borderWidth: selectedCollectible.rarity === 'Legendary' ? '3px' : '2px',
                  borderColor: rarityBorderColor[selectedCollectible.rarity] ?? '#a08060',
                  boxShadow: selectedCollectible.rarity === 'Legendary'
                    ? `0 0 80px ${rarityGlowColor['Legendary']}, 0 0 160px ${rarityGlowColor['Legendary'].replace('0.9', '0.3')}`
                    : `0 0 60px ${rarityGlowColor[selectedCollectible.rarity] ?? 'rgba(120,110,90,0.3)'}`,
                }}
              >
                <img src={selectedCollectible.imageUrl} alt={selectedCollectible.name} className="w-full h-full object-cover" />
              </motion.div>
              <div className="w-px h-10" style={{
                background: `linear-gradient(to bottom, ${rarityBorderColor[selectedCollectible.rarity] ?? 'rgba(196,164,122,0.6)'}, transparent)`
              }} />
              <div className="w-48 h-px" style={{ backgroundColor: selectedCollectible.rarity === 'Legendary' ? 'rgba(250,204,21,0.4)' : 'rgba(196,164,122,0.4)' }} />
              <div className="w-32 h-px mt-0.5" style={{ backgroundColor: selectedCollectible.rarity === 'Legendary' ? 'rgba(250,204,21,0.2)' : 'rgba(196,164,122,0.25)' }} />
              <div className="mt-8 bg-black/50 backdrop-blur-sm px-10 py-8"
                style={{ border: `1px solid ${selectedCollectible.rarity === 'Legendary' ? 'rgba(250,204,21,0.3)' : 'rgba(55,45,35,1)'}` }}>
                {selectedCollectible.rarity === 'Legendary' && (
                  <motion.p
                    animate={{ opacity: [0.6, 1, 0.6] }}
                    transition={{ duration: 2, repeat: Infinity }}
                    className="text-xs tracking-[0.5em] uppercase mb-3"
                    style={{ color: '#facc15', fontFamily: 'Cinzel, serif' }}
                  >
                    ★ Era Grandmaster ★
                  </motion.p>
                )}
                <p className="text-amber-50 font-bold mb-2"
                  style={{ fontFamily: 'Cinzel, serif', fontSize: selectedCollectible.rarity === 'Legendary' ? '2rem' : '1.875rem' }}>
                  {selectedCollectible.name}
                </p>
                <p className={`text-sm tracking-[0.3em] uppercase mb-6 ${rarityTextModal[selectedCollectible.rarity] ?? 'text-stone-400'}`}
                  style={{ fontFamily: 'Cinzel, serif' }}>
                  {selectedCollectible.rarity} Collectible
                </p>
                <p className="text-stone-300 text-base leading-relaxed mb-8"
                  style={{ fontFamily: 'EB Garamond, serif', fontSize: '1.1rem' }}>
                  {selectedCollectible.description}
                </p>
                <button onClick={() => setSelectedCollectible(null)}
                  className="text-stone-500 text-sm tracking-widest uppercase hover:text-amber-200 transition-colors cursor-pointer"
                  style={{ fontFamily: 'Cinzel, serif' }}>
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