import { Routes, Route, useLocation } from 'react-router-dom'
import { useEffect } from 'react'
import Layout from './components/Layout.tsx'
import HomePage from './pages/Home/HomePage.tsx'
import LoginPage from './pages/Auth/LoginPage.tsx'
import RegisterPage from './pages/Auth/RegisterPage.tsx'
import EraPage from './pages/Era/EraPage.tsx'
import ChapterPage from './pages/Chapter/ChapterPage.tsx'
import ProfilePage from './pages/Profile/ProfilePage.tsx'

function ScrollToTop() {
  const { pathname } = useLocation()
  useEffect(() => {
    window.scrollTo(0, 0)
  }, [pathname])
  return null
}

function App() {
  return (
    <Layout>
      <ScrollToTop />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/eras/:id" element={<EraPage />} />
        <Route path="/chapters/:id" element={<ChapterPage />} />
        <Route path="/profile" element={<ProfilePage />} />
      </Routes>
    </Layout>
  )
}

export default App