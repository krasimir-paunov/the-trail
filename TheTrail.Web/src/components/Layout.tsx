import type { ReactNode } from 'react'
import Navbar from './Navbar.tsx'
import Footer from './Footer.tsx'

interface LayoutProps {
  children: ReactNode
}

export default function Layout({ children }: LayoutProps) {
  return (
    <>
      <Navbar style="drawer" />
      {children}
      <Footer />
    </>
  )
}