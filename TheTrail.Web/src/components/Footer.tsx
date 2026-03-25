import { motion } from 'framer-motion'
import { Compass } from 'lucide-react'
import { useNavigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext.tsx'
import { useEffect, useState } from 'react'
import { erasApi } from '../api/erasApi.ts'
import type { EraDto } from '../types/index.ts'

export default function Footer() {
  const navigate = useNavigate()
  const { isAuthenticated, user, logout } = useAuth()
  const [eras, setEras] = useState<EraDto[]>([])

  useEffect(() => {
    erasApi.getAll().then(setEras).catch(() => {})
  }, [])

  const accountLinks = isAuthenticated
    ? [
        { label: user?.displayName ?? 'Profile', path: '/profile' },
        { label: 'My Collection', path: '/profile' },
      ]
    : [
        { label: 'Login', path: '/login' },
        { label: 'Register', path: '/register' },
      ]

  return (
    <motion.footer
      initial={{ opacity: 0 }}
      whileInView={{ opacity: 1 }}
      viewport={{ once: true }}
      transition={{ duration: 1 }}
      className="border-t border-stone-800 bg-stone-950"
    >
      <div className="max-w-6xl mx-auto px-4 py-16">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-12 mb-16">

          {/* Brand */}
          <div>
            <div className="flex items-center gap-3 mb-4">
              <Compass size={20} className="text-amber-200/60" />
              <span className="text-amber-50 font-bold text-xl">The Trail</span>
            </div>
            <p className="text-stone-500 text-sm leading-relaxed mb-4">
              An interactive encyclopedia where users journey through the most defining moments in human history.
            </p>
            <p className="text-amber-200/30 text-xs tracking-[0.3em] uppercase">
              Follow the trail of human history
            </p>
          </div>

          {/* Explore — dynamic era links */}
          <div>
            <p className="text-stone-400 text-xs tracking-[0.3em] uppercase mb-6">
              Explore
            </p>
            <ul className="flex flex-col gap-3">
              {eras.map((era) => (
                <li key={era.id}>
                  <button
                    onClick={() => navigate(`/eras/${era.id}`)}
                    className="text-stone-500 text-sm hover:text-amber-200/80 transition-colors duration-300 cursor-pointer text-left"
                  >
                    {era.name}
                  </button>
                </li>
              ))}
            </ul>
          </div>

          {/* Account */}
          <div>
            <p className="text-stone-400 text-xs tracking-[0.3em] uppercase mb-6">
              Account
            </p>
            <ul className="flex flex-col gap-3">
              {accountLinks.map((link) => (
                <li key={link.label}>
                  <button
                    onClick={() => navigate(link.path)}
                    className="text-stone-500 text-sm hover:text-amber-200/80 transition-colors duration-300 cursor-pointer text-left"
                  >
                    {link.label}
                  </button>
                </li>
              ))}
              {isAuthenticated && (
                <li>
                  <button
                    onClick={() => {
                      logout()
                      navigate('/')
                    }}
                    className="text-stone-600 text-sm hover:text-red-400/80 transition-colors duration-300 cursor-pointer text-left"
                  >
                    Logout
                  </button>
                </li>
              )}
            </ul>
          </div>

        </div>

        {/* Bottom bar */}
        <div className="border-t border-stone-800 pt-8 flex flex-col md:flex-row items-center justify-between gap-4">
          <p className="text-stone-600 text-xs tracking-wide">
            © 2026 The Trail. Built with passion for history.
          </p>
          <p className="text-stone-700 text-xs tracking-[0.2em] uppercase">
            Explore • Learn • Collect
          </p>
        </div>
      </div>
    </motion.footer>
  )
}