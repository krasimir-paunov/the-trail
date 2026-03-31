import { useEffect, useRef } from 'react'
import { motion, AnimatePresence } from 'framer-motion'
import { useNavigate } from 'react-router-dom'

interface Props {
  show: boolean
  eraName: string
  legendaryName: string
  legendaryDescription: string
  legendaryImageUrl: string | null
}

export default function EraGrandmasterModal({
  show, eraName, legendaryName, legendaryDescription, legendaryImageUrl
}: Props) {
  const navigate = useNavigate()
  const canvasRef = useRef<HTMLCanvasElement>(null)
  const animFrameRef = useRef<number>(0)

  useEffect(() => {
    if (!show) return
    const canvas = canvasRef.current
    if (!canvas) return
    const ctx = canvas.getContext('2d')!

    const resize = () => {
      canvas.width = canvas.offsetWidth
      canvas.height = canvas.offsetHeight
    }
    resize()

    const COLORS = ['#d4a853', '#f5e8c8', '#e8bf70', '#c4a47a', '#fff8e0', '#b8860b']
    let particles: {
      x: number; y: number; vx: number; vy: number
      life: number; decay: number; size: number
      color: string; rot: number; rotV: number; type: 'rect' | 'circle'
    }[] = []

    const burst = () => {
      const cx = canvas.width / 2
      const cy = canvas.height * 0.38
      for (let i = 0; i < 140; i++) {
        const angle = Math.random() * Math.PI * 2
        const speed = 3 + Math.random() * 9
        particles.push({
          x: cx, y: cy,
          vx: Math.cos(angle) * speed,
          vy: Math.sin(angle) * speed - Math.random() * 4,
          life: 1,
          decay: 0.006 + Math.random() * 0.012,
          size: 2 + Math.random() * 6,
          color: COLORS[Math.floor(Math.random() * COLORS.length)],
          rot: Math.random() * Math.PI * 2,
          rotV: (Math.random() - 0.5) * 0.25,
          type: Math.random() > 0.45 ? 'rect' : 'circle'
        })
      }
    }

    const draw = () => {
      ctx.clearRect(0, 0, canvas.width, canvas.height)
      particles = particles.filter(p => p.life > 0)

      // Gentle rain after burst
      if (Math.random() > 0.65) {
        particles.push({
          x: Math.random() * canvas.width, y: -8,
          vx: (Math.random() - 0.5),
          vy: 1 + Math.random() * 2,
          life: 1, decay: 0.004 + Math.random() * 0.006,
          size: 1.5 + Math.random() * 4,
          color: COLORS[Math.floor(Math.random() * COLORS.length)],
          rot: Math.random() * Math.PI * 2,
          rotV: (Math.random() - 0.5) * 0.2,
          type: Math.random() > 0.45 ? 'rect' : 'circle'
        })
      }

      for (const p of particles) {
        ctx.globalAlpha = p.life * 0.85
        ctx.fillStyle = p.color
        p.rot += p.rotV
        if (p.type === 'circle') {
          ctx.beginPath()
          ctx.arc(p.x, p.y, p.size / 2, 0, Math.PI * 2)
          ctx.fill()
        } else {
          ctx.save()
          ctx.translate(p.x, p.y)
          ctx.rotate(p.rot)
          ctx.fillRect(-p.size / 2, -p.size / 2, p.size, p.size * 0.5)
          ctx.restore()
        }
        p.x += p.vx; p.y += p.vy
        p.vy += 0.09; p.vx *= 0.99
        p.life -= p.decay
      }
      ctx.globalAlpha = 1
      animFrameRef.current = requestAnimationFrame(draw)
    }

    setTimeout(burst, 300)
    draw()

    return () => cancelAnimationFrame(animFrameRef.current)
  }, [show])

  return (
    <AnimatePresence>
      {show && (
        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          exit={{ opacity: 0 }}
          className="fixed inset-0 z-[60] flex items-center justify-center px-4"
          style={{ background: 'rgba(13,10,6,0.97)', backdropFilter: 'blur(8px)' }}
        >
          <canvas
            ref={canvasRef}
            className="absolute inset-0 w-full h-full pointer-events-none"
          />

          <div className="relative z-10 text-center max-w-md w-full">

            {/* Era label */}
            <motion.p
              initial={{ opacity: 0, y: 14 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: 0.2 }}
              className="text-xs tracking-[0.4em] uppercase mb-1"
              style={{ color: '#d4a853', fontFamily: "'Cinzel', serif" }}
            >
              Era Mastered
            </motion.p>

            {/* Title */}
            <motion.h2
              initial={{ opacity: 0, y: 14 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: 0.5 }}
              className="text-5xl font-black mb-6"
              style={{ color: '#f5e8c8', fontFamily: "'Cinzel', serif" }}
            >
              Era <span style={{ color: '#d4a853' }}>Grandmaster</span>
            </motion.h2>

            {/* Collectible image — the centrepiece */}
            <motion.div
              initial={{ opacity: 0, scale: 0.75 }}
              animate={{ opacity: 1, scale: 1 }}
              transition={{ delay: 0.9, duration: 0.6, ease: [0.34, 1.56, 0.64, 1] }}
              className="relative inline-block mb-5"
            >
              {/* Outer slow ring */}
              <motion.div
                animate={{ rotate: 360 }}
                transition={{ duration: 8, repeat: Infinity, ease: 'linear' }}
                className="absolute inset-[-10px] rounded-full"
                style={{ border: '0.5px solid rgba(212,168,83,0.2)' }}
              />
              {/* Inner fast ring */}
              <motion.div
                animate={{ rotate: -360 }}
                transition={{ duration: 4, repeat: Infinity, ease: 'linear' }}
                className="absolute inset-[-5px] rounded-full"
                style={{
                  border: '1.5px solid transparent',
                  borderTopColor: '#d4a853',
                  borderRightColor: 'rgba(212,168,83,0.35)',
                  borderBottomColor: '#d4a853',
                  borderLeftColor: 'rgba(212,168,83,0.35)',
                }}
              />
              {/* Image */}
              <div
                className="relative w-44 h-44 rounded-full overflow-hidden"
                style={{ border: '2px solid rgba(212,168,83,0.5)' }}
              >
                {legendaryImageUrl ? (
                  <img
                    src={legendaryImageUrl}
                    alt={legendaryName}
                    className="w-full h-full object-cover"
                  />
                ) : (
                  <div
                    className="w-full h-full flex items-center justify-center text-6xl"
                    style={{ background: '#1a1208' }}
                  >
                    ★
                  </div>
                )}
              </div>
            </motion.div>

            {/* Legendary tag */}
            <motion.p
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: 1.3 }}
              className="text-xs tracking-[0.3em] uppercase mb-2"
              style={{ color: '#d4a853', fontFamily: "'Cinzel', serif" }}
            >
              ★ Legendary Collectible Unlocked
            </motion.p>

            {/* Collectible name */}
            <motion.p
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: 1.5 }}
              className="text-2xl font-bold mb-3"
              style={{ color: '#f5e8c8', fontFamily: "'Cinzel', serif" }}
            >
              {legendaryName}
            </motion.p>

            {/* Divider */}
            <motion.div
              initial={{ width: 0, opacity: 0 }}
              animate={{ width: 40, opacity: 1 }}
              transition={{ delay: 1.55 }}
              className="mx-auto mb-4"
              style={{ height: 1, background: '#d4a853' }}
            />

            {/* Description */}
            <motion.p
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: 1.65 }}
              className="text-lg italic mb-5 px-4"
              style={{ color: '#c4a47a', fontFamily: "'EB Garamond', serif", lineHeight: 1.65 }}
            >
              "{legendaryDescription}"
            </motion.p>

            {/* Era chips */}
            <motion.div
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: 1.75 }}
              className="flex items-center justify-center gap-3 mb-6"
            >
              {[eraName, 'All 5 Chapters Complete'].map(label => (
                <span
                  key={label}
                  className="text-xs tracking-widest rounded-full px-4 py-1"
                  style={{
                    fontFamily: "'Cinzel', serif",
                    color: '#c4a47a',
                    border: '1px solid rgba(196,164,122,0.3)'
                  }}
                >
                  {label}
                </span>
              ))}
            </motion.div>

            {/* CTA */}
            <motion.button
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: 1.9 }}
              whileHover={{ scale: 1.03 }}
              whileTap={{ scale: 0.98 }}
              onClick={() => navigate('/profile')}
              className="px-10 py-4 text-xs tracking-[0.25em] uppercase font-bold cursor-pointer"
              style={{
                background: '#d4a853',
                color: '#1a0f05',
                border: 'none',
                borderRadius: 4,
                fontFamily: "'Cinzel', serif",
              }}
            >
              View My Profile
            </motion.button>
          </div>
        </motion.div>
      )}
    </AnimatePresence>
  )
}