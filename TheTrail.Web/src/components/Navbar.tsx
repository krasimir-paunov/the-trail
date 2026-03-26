import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { motion, AnimatePresence } from 'framer-motion'
import { X } from 'lucide-react'
import { useAuth } from '../context/AuthContext.tsx'

type NavStyle = 'fullscreen' | 'drawer'

interface NavbarProps {
  style?: NavStyle
}

export default function Navbar({ style = 'fullscreen' }: NavbarProps) {
  const [isOpen, setIsOpen] = useState(false)
  const [scrolled, setScrolled] = useState(false)
  const [hovered, setHovered] = useState(false)
  const { isAuthenticated, user, logout } = useAuth()
  const navigate = useNavigate()

  useEffect(() => {
    const handleScroll = () => setScrolled(window.scrollY > 50)
    window.addEventListener('scroll', handleScroll)
    return () => window.removeEventListener('scroll', handleScroll)
  }, [])

  const handleNavigate = (path: string) => {
    setIsOpen(false)
    navigate(path)
  }

  const handleLogout = () => {
    logout()
    setIsOpen(false)
    navigate('/')
  }

  return (
    <>
      {/* ── Trigger button — large compass rose with glow on hover ── */}
      <motion.button
        onClick={() => setIsOpen(!isOpen)}
        animate={{ opacity: isOpen ? 0 : 1, pointerEvents: isOpen ? 'none' : 'auto' }}
        onMouseEnter={() => setHovered(true)}
        onMouseLeave={() => setHovered(false)}
        whileTap={{ scale: 0.92 }}
        className="fixed top-4 right-4 z-50 cursor-pointer"
        style={{ background: 'none', border: 'none', padding: 0, width: '72px', height: '72px' }}
      >
        {/* Glow ring behind image — animates on hover */}
        <motion.div
          animate={{
            opacity: hovered ? 1 : 0,
            scale: hovered ? 1.15 : 0.9,
          }}
          transition={{ duration: 0.35, ease: 'easeOut' }}
          style={{
            position: 'absolute',
            inset: '-8px',
            borderRadius: '50%',
            background: 'radial-gradient(circle, rgba(212,168,83,0.35) 0%, transparent 70%)',
            filter: 'blur(8px)',
            pointerEvents: 'none',
          }}
        />
        {/* Secondary tight glow */}
        <motion.div
          animate={{ opacity: hovered ? 0.8 : 0 }}
          transition={{ duration: 0.3 }}
          style={{
            position: 'absolute',
            inset: '0',
            borderRadius: '50%',
            boxShadow: '0 0 24px 8px rgba(212,168,83,0.4)',
            pointerEvents: 'none',
          }}
        />
        <motion.img
          src="/images/nav-button.png"
          alt="Navigation"
          animate={{
            scale: hovered ? 1.08 : 1,
            filter: hovered
              ? 'brightness(1.3) drop-shadow(0 0 12px rgba(212,168,83,0.7))'
              : scrolled
                ? 'brightness(1.0) drop-shadow(0 0 6px rgba(212,168,83,0.3))'
                : 'brightness(0.85) drop-shadow(0 0 4px rgba(212,168,83,0.2))',
          }}
          transition={{ duration: 0.35, ease: 'easeOut' }}
          style={{ width: '100%', height: '100%', objectFit: 'contain', display: 'block' }}
        />
      </motion.button>

      <AnimatePresence>
        {isOpen && (
          <>
            {style === 'fullscreen' ? (
              <motion.div
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                exit={{ opacity: 0 }}
                transition={{ duration: 0.4 }}
                className="fixed inset-0 z-40 flex flex-col items-center justify-center"
              >
                <div className="absolute inset-0">
                  <img src="/images/HeroMap.jpg" alt=""
                    className="w-full h-full object-cover"
                    style={{ objectPosition: '50% 40%' }} />
                  <div className="absolute inset-0 bg-black/85" />
                </div>

                <motion.button
                  initial={{ opacity: 0, rotate: -90 }}
                  animate={{ opacity: 1, rotate: 0 }}
                  exit={{ opacity: 0 }}
                  transition={{ delay: 0.2 }}
                  onClick={() => setIsOpen(false)}
                  className="absolute top-6 right-6 p-3 text-amber-200/60 hover:text-amber-200 transition-colors cursor-pointer"
                >
                  <X size={24} />
                </motion.button>

                <motion.p
                  initial={{ opacity: 0, y: -20 }}
                  animate={{ opacity: 1, y: 0 }}
                  transition={{ delay: 0.1 }}
                  className="relative z-10 text-amber-200/40 text-xs tracking-[0.4em] uppercase mb-4"
                >
                  An interactive history encyclopedia
                </motion.p>

                <motion.h1
                  initial={{ opacity: 0, y: -10 }}
                  animate={{ opacity: 1, y: 0 }}
                  transition={{ delay: 0.15 }}
                  className="relative z-10 text-4xl font-bold text-amber-50 mb-16 cursor-pointer"
                  onClick={() => handleNavigate('/')}
                >
                  The Trail
                </motion.h1>

                <nav className="relative z-10 flex flex-col items-center gap-6">
                  {[
                    { label: 'Home', path: '/' },
                    { label: 'Explore Eras', path: '/#eras' },
                    ...(isAuthenticated ? [{ label: 'My Profile', path: '/profile' }] : []),
                  ].map((link, i) => (
                    <motion.button
                      key={link.path}
                      initial={{ opacity: 0, y: 20 }}
                      animate={{ opacity: 1, y: 0 }}
                      transition={{ delay: 0.2 + i * 0.08 }}
                      onClick={() => handleNavigate(link.path)}
                      className="text-3xl md:text-4xl font-bold text-stone-300 hover:text-amber-200 transition-colors duration-300 tracking-wide cursor-pointer"
                    >
                      {link.label}
                    </motion.button>
                  ))}
                </nav>

                <motion.div
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  transition={{ delay: 0.5 }}
                  className="relative z-10 flex gap-6 mt-16"
                >
                  {isAuthenticated ? (
                    <>
                      <span className="text-stone-500 text-sm tracking-widest uppercase self-center">
                        {user?.displayName}
                      </span>
                      <button onClick={handleLogout}
                        className="px-6 py-2 border border-stone-600 text-stone-400 text-sm tracking-widest uppercase hover:border-amber-200/60 hover:text-amber-200 transition-all duration-300 cursor-pointer">
                        Logout
                      </button>
                    </>
                  ) : (
                    <>
                      <button onClick={() => handleNavigate('/login')}
                        className="px-6 py-2 text-stone-400 text-sm tracking-widest uppercase hover:text-amber-200 transition-colors duration-300 cursor-pointer">
                        Login
                      </button>
                      <button onClick={() => handleNavigate('/register')}
                        className="px-6 py-2 border border-amber-200/30 text-amber-200/70 text-sm tracking-widest uppercase hover:border-amber-200/60 hover:text-amber-200 transition-all duration-300 cursor-pointer">
                        Begin Your Journey
                      </button>
                    </>
                  )}
                </motion.div>
              </motion.div>
            ) : (
              <>
                <motion.div
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  exit={{ opacity: 0 }}
                  onClick={() => setIsOpen(false)}
                  className="fixed inset-0 z-40 bg-black/60 backdrop-blur-sm cursor-pointer"
                />
                <motion.div
                  initial={{ x: '100%' }}
                  animate={{ x: 0 }}
                  exit={{ x: '100%' }}
                  transition={{ duration: 0.4, ease: [0.32, 0.72, 0, 1] }}
                  className="fixed top-0 right-0 h-full w-80 z-50 flex flex-col"
                >
                  <div className="absolute inset-0">
                    <img src="/images/HeroMap.jpg" alt=""
                      className="w-full h-full object-cover"
                      style={{ objectPosition: '20% 40%' }} />
                    <div className="absolute inset-0 bg-stone-950/92" />
                  </div>

                  <div className="relative z-10 flex justify-between items-center p-6 border-b border-stone-800">
                    <div className="cursor-pointer" onClick={() => handleNavigate('/')}>
                      <span className="text-amber-50 font-bold text-xl block">The Trail</span>
                      <span className="text-amber-200/40 text-xs tracking-[0.2em] uppercase">Follow the trail</span>
                    </div>
                    <button onClick={() => setIsOpen(false)}
                      className="text-stone-400 hover:text-amber-200 transition-colors cursor-pointer">
                      <X size={20} />
                    </button>
                  </div>

                  <nav className="relative z-10 flex flex-col p-6 gap-2 flex-1">
                    {[
                      { label: 'Home', path: '/' },
                      { label: 'Explore Eras', path: '/#eras' },
                      ...(isAuthenticated ? [{ label: 'My Profile', path: '/profile' }] : []),
                    ].map((link, i) => (
                      <motion.button
                        key={link.path}
                        initial={{ opacity: 0, x: 20 }}
                        animate={{ opacity: 1, x: 0 }}
                        transition={{ delay: 0.1 + i * 0.06 }}
                        onClick={() => handleNavigate(link.path)}
                        className="text-left text-lg text-stone-300 hover:text-amber-200 transition-colors duration-300 py-3 border-b border-stone-800/50 tracking-wide cursor-pointer"
                      >
                        {link.label}
                      </motion.button>
                    ))}
                  </nav>

                  <motion.div
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    transition={{ delay: 0.3 }}
                    className="relative z-10 p-6 border-t border-stone-800 flex flex-col gap-3"
                  >
                    {isAuthenticated ? (
                      <>
                        <p className="text-amber-200/60 text-m tracking-wide">{user?.displayName}</p>
                        <button onClick={handleLogout}
                          className="w-full py-2 border border-stone-700 text-stone-400 text-sm tracking-widest uppercase hover:border-amber-200/60 hover:text-amber-200 transition-all duration-300 cursor-pointer">
                          Logout
                        </button>
                      </>
                    ) : (
                      <>
                        <button onClick={() => handleNavigate('/login')}
                          className="w-full py-2 text-stone-400 text-sm tracking-widest uppercase hover:text-amber-200 transition-colors border border-stone-800 hover:border-stone-600 cursor-pointer">
                          Login
                        </button>
                        <button onClick={() => handleNavigate('/register')}
                          className="w-full py-2 border border-amber-200/30 text-amber-200/70 text-sm tracking-widest uppercase hover:border-amber-200/60 hover:text-amber-200 transition-all cursor-pointer">
                          Begin Your Journey
                        </button>
                      </>
                    )}
                  </motion.div>
                </motion.div>
              </>
            )}
          </>
        )}
      </AnimatePresence>
    </>
  )
}