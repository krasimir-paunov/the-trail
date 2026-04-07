import { useEffect, useState } from 'react'
import { motion } from 'framer-motion'

interface WikiSummary {
  title: string
  extract: string
  thumbnail?: { source: string }
  content_urls?: { desktop: { page: string } }
}

interface Props {
  slug: string
}

export default function DiveDeeperSection({ slug }: Props) {
  const [wiki, setWiki] = useState<WikiSummary | null>(null)
  const [loading, setLoading] = useState(true)
  const [failed, setFailed] = useState(false)

  useEffect(() => {
    if (!slug) return
    setLoading(true)
    setFailed(false)

    fetch(`https://en.wikipedia.org/api/rest_v1/page/summary/${encodeURIComponent(slug)}`)
      .then(r => {
        if (!r.ok) throw new Error('Not found')
        return r.json()
      })
      .then((data: WikiSummary) => {
        setWiki(data)
        setLoading(false)
      })
      .catch(() => {
        setFailed(true)
        setLoading(false)
      })
  }, [slug])

  if (loading || failed || !wiki) return null

  const extract = wiki.extract.length > 400
    ? wiki.extract.slice(0, 400).trimEnd() + '...'
    : wiki.extract

  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      whileInView={{ opacity: 1, y: 0 }}
      viewport={{ once: true }}
      transition={{ duration: 0.7 }}
      className="mt-16 mb-4"
      style={{ borderTop: '1px solid var(--parchment-border)' }}
    >
      <div className="flex items-center gap-4 pt-10 mb-8">
        <div className="h-px flex-1" style={{ background: 'var(--parchment-border)' }} />
        <p
          className="text-xs tracking-[0.4em] uppercase"
          style={{ color: 'var(--accent-amber)', fontFamily: "'Cinzel', serif" }}
        >
          Dive Deeper
        </p>
        <div className="h-px flex-1" style={{ background: 'var(--parchment-border)' }} />
      </div>

      <div
        className="flex gap-6 p-6"
        style={{
          background: 'var(--parchment-mid)',
          borderLeft: '3px solid var(--accent-amber)',
        }}
      >
        {wiki.thumbnail?.source && (
          <div className="shrink-0 hidden md:block">
            <img
              src={wiki.thumbnail.source}
              alt={wiki.title}
              className="object-cover"
              style={{
                width: 120,
                height: 120,
                border: '1px solid var(--parchment-border)',
              }}
            />
          </div>
        )}

        <div className="flex-1 min-w-0">
          <div className="flex items-center gap-3 mb-3">
            <p
              className="text-xs tracking-[0.25em] uppercase"
              style={{ color: 'var(--ink-muted)', fontFamily: "'Cinzel', serif" }}
            >
              Wikipedia
            </p>
            <div className="h-px flex-1" style={{ background: 'var(--parchment-border)' }} />
          </div>

          <h3
            className="text-xl font-bold mb-3"
            style={{ color: 'var(--ink-dark)', fontFamily: "'Cinzel', serif" }}
          >
            {wiki.title}
          </h3>

          <p
            className="text-base leading-relaxed mb-4"
            style={{ color: 'var(--ink-medium)', fontFamily: "'EB Garamond', serif" }}
          >
            {extract}
          </p>

          {wiki.content_urls?.desktop?.page && (
            
              <a href={wiki.content_urls.desktop.page}
              target="_blank"
              rel="noopener noreferrer"
              className="inline-flex items-center gap-2 text-sm tracking-widest uppercase transition-opacity duration-200 hover:opacity-70"
              style={{ color: 'var(--accent-amber)', fontFamily: "'Cinzel', serif" }}
            >
              Read on Wikipedia →
            </a>
          )}
        </div>
      </div>
    </motion.div>
  )
}