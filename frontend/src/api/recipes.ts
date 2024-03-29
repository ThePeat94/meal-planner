import {
	createQuery,
	type CreateMutationResult,
	type CreateQueryResult,
	createMutation,
	useQueryClient,
} from '@tanstack/svelte-query';
import axios, { type AxiosRequestConfig } from 'axios';

export type Recipe = {
	id: number;
	name: string;
	description?: string;
};

export type CreateRecipe = {
	name: string;
};

const fetchRecipes = async (config: AxiosRequestConfig): Promise<Recipe[]> => {
	const request = await axios.get<Recipe[]>('http://localhost:5190/recipes', { ...config });
	return request.data;
};

const fetchRecipe = async (id: number, config: AxiosRequestConfig): Promise<Recipe> => {
	const request = await axios.get<Recipe>('http://localhost:5190/recipes/' + id, { ...config });
	return request.data;
};

export const getAllRecipes = (): CreateQueryResult<Recipe[]> => {
	return createQuery<Recipe[]>({
		queryKey: ['recipes'],
		queryFn: async ({ signal }) => await fetchRecipes({ signal }),
	});
};

const postRecipe = async (name: string, config?: AxiosRequestConfig): Promise<Recipe> => {
	const request = await axios.post<Recipe>(
		'http://localhost:5190/recipes',
		{ name },
		{ ...config },
	);
	return request.data;
};

const putRecipe = async (recipe: Recipe, config?: AxiosRequestConfig): Promise<Recipe> => {
	const request = await axios.put<Recipe>(
		'http://localhost:5190/recipes/',
		{ ...recipe },
		{ ...config },
	);
	return request.data;
};

export const createRecipe = (): CreateMutationResult<Recipe, Error, CreateRecipe> => {
	const client = useQueryClient();
	return createMutation<Recipe, Error, CreateRecipe>({
		mutationFn: async (r) => await postRecipe(r.name),
		onSettled: () => client.invalidateQueries({ queryKey: ['recipes'] }),
	});
};

export const getRecipe = (id: number): CreateQueryResult<Recipe> => {
	return createQuery<Recipe>({
		queryKey: ['recipes', id],
		queryFn: async ({ signal }) => await fetchRecipe(id, { signal }),
	});
};

export const updateRecipe = (): CreateMutationResult<Recipe, Error, Recipe> => {
	const client = useQueryClient();
	return createMutation<Recipe, Error, Recipe>({
		mutationFn: async (r) => await putRecipe(r),
		onSettled: (r) => client.invalidateQueries({ queryKey: ['recipes', r?.id ?? []] }),
	});
};
