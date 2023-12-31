<script lang="ts">
	import 'app.pcss';
	import { browser } from '$app/environment';
	import { QueryClient, QueryClientProvider } from '@tanstack/svelte-query';
	import { SvelteQueryDevtools } from '@tanstack/svelte-query-devtools';
	import { AppRail, AppRailAnchor, AppShell, Modal } from '@skeletonlabs/skeleton';
	import axios from 'axios';
	import { page } from '$app/stores';
	import { initializeStores } from '@skeletonlabs/skeleton';
	import CreateRecipeDialog from 'dialogs/CreateRecipeDialog.svelte';
	import CreateMealDialog from 'dialogs/CreateMealDialog.svelte';

	initializeStores();

	const queryClient = new QueryClient({
		defaultOptions: {
			queries: {
				enabled: browser,
			},
		},
	});

	type Route = {
		name: string;
		link: string;
	};

	const routes: Route[] = [
		{
			name: 'Home',
			link: '/',
		},
		{
			name: 'Meal Plan',
			link: '/mealplans',
		},
		{
			name: 'Recipe',
			link: '/recipes',
		},
	];

	axios.defaults.baseURL = 'localhost:5190';
	axios.defaults.headers.post['Content-Type'] = 'application/json';
	axios.defaults.headers.post['Accept'] = 'application/json';
	axios.defaults.headers.get['Accept'] = 'application/json';
	axios.defaults.headers.get['Content-Type'] = 'application/json';
</script>

<QueryClientProvider client={queryClient}>
	<Modal
		components={{
			createRecipeDialog: { ref: CreateRecipeDialog },
			createMealDialog: { ref: CreateMealDialog },
		}}
	/>
	<AppShell slotSidebarLeft="bg-surface-500/5 p-4">
		<svelte:fragment slot="sidebarLeft">
			<AppRail>
				<svelte:fragment slot="lead">
					{#each routes as route}
						<AppRailAnchor
							href={route.link}
							title={route.name}
							selected={$page.url.pathname === route.link}
						>
							{route.name}
						</AppRailAnchor>
					{/each}
				</svelte:fragment>
			</AppRail>
		</svelte:fragment>
		<div class="p-4">
			<slot />
		</div>
	</AppShell>
	<SvelteQueryDevtools />
</QueryClientProvider>
