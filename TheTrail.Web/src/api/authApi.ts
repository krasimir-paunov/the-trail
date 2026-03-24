import apiClient from './client';
import type { AuthResponseDto, LoginDto, RegisterDto } from '../types';

export const authApi = {
  login: async (dto: LoginDto): Promise<AuthResponseDto> => {
    const response = await apiClient.post<AuthResponseDto>('/api/auth/login', dto);
    return response.data;
  },

  register: async (dto: RegisterDto): Promise<AuthResponseDto> => {
    const response = await apiClient.post<AuthResponseDto>('/api/auth/register', dto);
    return response.data;
  },
};