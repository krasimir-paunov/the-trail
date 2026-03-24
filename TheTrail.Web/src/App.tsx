import { Routes, Route } from 'react-router-dom'
import Layout from './components/Layout.tsx'
import HomePage from './pages/Home/HomePage.tsx'
import LoginPage from './pages/Auth/LoginPage.tsx'
import RegisterPage from './pages/Auth/RegisterPage.tsx'
import EraPage from './pages/Era/EraPage.tsx'
import ChapterPage from './pages/Chapter/ChapterPage.tsx'

function App() {
  return (
    <Layout>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/eras/:id" element={<EraPage />} />
        <Route path="/chapters/:id" element={<ChapterPage />} />
      </Routes>
    </Layout>
  )
}

export default App