import { createQuery, type CreateQueryResult } from '@tanstack/svelte-query';
import axios, { type AxiosRequestConfig } from 'axios';

export type Recipe = {
    id?: number;
    name: string;
    description?: string;
}

const fetchRecipes = async (config: AxiosRequestConfig): Promise<Recipe[]> => {
    const request = await axios.get<Recipe[]>('http://localhost:5190/recipes', { ...config });
    return request.data;
};

export const getAllRecipes = () : CreateQueryResult<Recipe[]> => {
    return createQuery<Recipe[]>({
        queryKey: ['recipes'],
        queryFn: async ({ signal }) => await fetchRecipes({ signal }),
    });
};