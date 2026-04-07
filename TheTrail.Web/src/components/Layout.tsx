import { useEffect, useState } from 'react'
import type { ReactNode } from 'react'
import { motion, AnimatePresence } from 'framer-motion'
import Navbar from './Navbar.tsx'
import Footer from './Footer.tsx'

interface LayoutProps {
  children: ReactNode
}

export default function Layout({ children }: LayoutProps) {
  const [showTop, setShowTop] = useState(false)

  useEffect(() => {
    const handleScroll = () => setShowTop(window.scrollY > 300)
    window.addEventListener('scroll', handleScroll)
    return () => window.removeEventListener('scroll', handleScroll)
  }, [])

  return (
    <>
      <Navbar style="drawer" />
      {children}
      <Footer />

      {/* Back to top */}
      <AnimatePresence>
        {showTop && (
          <motion.button
            initial={{ opacity: 0, scale: 0.8 }}
            animate={{ opacity: 1, scale: 1 }}
            exit={{ opacity: 0, scale: 0.8 }}
            whileHover={{ scale: 1.1 }}
            whileTap={{ scale: 0.95 }}
            onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })}
            className="fixed bottom-8 right-8 z-40 cursor-pointer flex items-center justify-center"
            style={{
              width: 48,
              height: 48,
              borderRadius: '50%',
              background: '#d4a853',
              border: 'none',
              boxShadow: '0 4px 20px rgba(212,168,83,0.4)',
              color: '#1a0f05',
              fontSize: '1.2rem',
              fontWeight: 700,
            }}
          >
            ↑
          </motion.button>
        )}
      </AnimatePresence>
    </>
  )
}