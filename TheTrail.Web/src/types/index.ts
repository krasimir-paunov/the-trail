export interface EraDto {
  id: number;
  name: string;
  description: string;
  coverImageUrl: string | null;
  colorTheme: string | null;
  order: number;
  chapterCount: number;
  completedChapterCount: number;
  isGrandmasterUnlocked: boolean;
}

export interface ChapterDto {
  id: number;
  title: string;
  subtitle: string;
  coverImageUrl: string | null;
  order: number;
  estimatedMinutes: number;
  eraId: number;
  scrollCompleted: boolean;
  quizPassed: boolean;
  hasQuiz: boolean;
  hasCollectible: boolean;
  collectibleRarity: string | null;
  collectibleName: string | null;
  collectibleDescription: string | null;
  collectibleImageUrl: string | null;
  rareCollectibleName: string | null;
  rareCollectibleDescription: string | null;
  rareCollectibleImageUrl: string | null;
  commonCollectibleEarned: boolean;
  rareCollectibleEarned: boolean;
  wikiSlug: string | null;
}

export interface AuthResponseDto {
  token: string;
  displayName: string;
  email: string;
  expiresAt: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  displayName: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface QuizQuestion {
  id: number
  text: string
  optionA: string
  optionB: string
  optionC: string
  optionD: string
  order: number
}