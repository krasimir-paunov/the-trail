import apiClient from './client.ts'
import type { ChapterDto, QuizQuestion } from '../types/index.ts'

interface QuizResultDto {
  passed: boolean
  perfectScore: boolean
  legendaryAwarded: boolean
  legendaryName: string | null
  legendaryDescription: string | null
  legendaryImageUrl: string | null
  eraName: string | null
}

export const chaptersApi = {
  getByEra: async (eraId: number): Promise<ChapterDto[]> => {
    const response = await apiClient.get<ChapterDto[]>(`/api/chapters/era/${eraId}`)
    return response.data
  },

  getById: async (id: number): Promise<ChapterDto> => {
    const response = await apiClient.get<ChapterDto>(`/api/chapters/${id}`)
    return response.data
  },

  completeScroll: async (chapterId: number): Promise<boolean> => {
    const response = await apiClient.post<boolean>(`/api/chapters/${chapterId}/complete-scroll`)
    return response.data
  },

  getContent: async (id: number): Promise<string> => {
    const response = await apiClient.get<string>(`/api/chapters/${id}/content`)
    return response.data
  },

  getQuiz: async (id: number): Promise<QuizQuestion[]> => {
    const response = await apiClient.get<{ questions: QuizQuestion[] }>(`/api/chapters/${id}/quiz`)
    return response.data.questions
  },

  saveQuizResult: async (chapterId: number, passed: boolean, perfectScore: boolean): Promise<QuizResultDto> => {
    const response = await apiClient.post<QuizResultDto>(`/api/chapters/${chapterId}/quiz/result`, { passed, perfectScore })
    return response.data
  },
}