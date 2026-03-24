import { useState } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import { motion } from 'framer-motion'
import { useAuth } from '../../context/AuthContext.tsx'
import { authApi } from '../../api/authApi.ts'

export default function LoginPage() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)
  const { login } = useAuth()
  const navigate = useNavigate()

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setError(null)
    setLoading(true)

    try {
      const response = await authApi.login({ email, password })
      login(response)
      navigate('/')
    } catch {
      setError('Invalid email or password.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="min-h-screen bg-stone-950 flex items-center justify-center px-4">
      {/* Background */}
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
          style={{
            background: 'radial-gradient(ellipse at center, transparent 20%, rgba(0,0,0,0.9) 100%)'
          }}
        />
      </div>

      {/* Form */}
      <motion.div
        initial={{ opacity: 0, y: 30 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.8 }}
        className="relative z-10 w-full max-w-md"
      >
        {/* Header */}
        <div className="text-center mb-10">
          <motion.p
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ delay: 0.3 }}
            className="text-amber-200/40 text-xs tracking-[0.4em] uppercase mb-3"
          >
            Welcome back
          </motion.p>
          <motion.h1
            initial={{ opacity: 0, y: -10 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.4 }}
            className="text-4xl font-bold text-amber-50"
          >
            The Trail
          </motion.h1>
        </div>

        {/* Card */}
        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.5 }}
          className="bg-stone-950/80 backdrop-blur-sm border border-stone-800 p-8"
        >
          <h2 className="text-stone-300 text-sm tracking-[0.3em] uppercase mb-8">
            Continue Your Journey
          </h2>

          <form onSubmit={handleSubmit} className="flex flex-col gap-5">
            <div>
              <label className="text-stone-500 text-xs tracking-widest uppercase block mb-2">
                Email
              </label>
              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
                className="w-full bg-stone-900/50 border border-stone-700 text-stone-200 px-4 py-3 text-sm focus:outline-none focus:border-amber-200/50 transition-colors"
                placeholder="your@email.com"
              />
            </div>

            <div>
              <label className="text-stone-500 text-xs tracking-widest uppercase block mb-2">
                Password
              </label>
              <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
                className="w-full bg-stone-900/50 border border-stone-700 text-stone-200 px-4 py-3 text-sm focus:outline-none focus:border-amber-200/50 transition-colors"
                placeholder="••••••••"
              />
            </div>

            {error && (
              <motion.p
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                className="text-red-400 text-xs tracking-wide"
              >
                {error}
              </motion.p>
            )}

            <motion.button
              type="submit"
              disabled={loading}
              whileHover={{ scale: loading ? 1 : 1.02 }}
              whileTap={{ scale: loading ? 1 : 0.98 }}
              className="w-full py-3 border border-amber-200/30 text-amber-200/80 text-sm tracking-widest uppercase hover:border-amber-200/60 hover:text-amber-50 transition-all duration-300 disabled:opacity-50 disabled:cursor-not-allowed mt-2"
            >
              {loading ? 'Entering...' : 'Enter The Trail'}
            </motion.button>
          </form>
        </motion.div>

        {/* Footer */}
        <motion.p
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.7 }}
          className="text-center text-stone-600 text-xs mt-6 tracking-wide"
        >
          New to The Trail?{' '}
          <Link
            to="/register"
            className="text-amber-200/60 hover:text-amber-200 transition-colors"
          >
            Begin your journey
          </Link>
        </motion.p>
      </motion.div>
    </div>
  )
}