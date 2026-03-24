import Navbar from './Navbar.tsx'

interface LayoutProps {
  children: React.ReactNode
}

export default function Layout({ children }: LayoutProps) {
  return (
    <>
      <Navbar style="drawer" />
      {children}
    </>
  )
}