import apiClient from './client';
import type { EraDto } from '../types';

export const erasApi = {
  getAll: async (): Promise<EraDto[]> => {
    const response = await apiClient.get<EraDto[]>('/api/eras');
    return response.data;
  },

  getById: async (id: number): Promise<EraDto> => {
    const response = await apiClient.get<EraDto>(`/api/eras/${id}`);
    return response.data;
  },
};