<script lang="ts">
	import '../app.pcss';
	import { browser } from '$app/environment';
	import { QueryClient, QueryClientProvider } from '@tanstack/svelte-query';
	import { SvelteQueryDevtools } from '@tanstack/svelte-query-devtools';
    import { AppShell } from '@skeletonlabs/skeleton';
	import axios from 'axios';

	const queryClient = new QueryClient({
		defaultOptions: {
			queries: {
				enabled: browser
			}
		}
	});

	axios.defaults.baseURL = 'localhost:5190';
	axios.defaults.headers.post['Content-Type'] = 'application/json';
	axios.defaults.headers.post['Accept'] = 'application/json';
	axios.defaults.headers.get['Accept'] = 'application/json';
	axios.defaults.headers.get['Content-Type'] = 'application/json';
</script>

<QueryClientProvider client={queryClient}>
    <AppShell slotSidebarLeft="bg-surface-500/5 w-56 p-4">
        <svelte:fragment slot="sidebarLeft">
            <!-- Insert the list: -->
            <nav class="list-nav">
                <ul>
                    <li><a href="/">Home</a></li>
                    <li><a href="/about">About</a></li>
                </ul>
            </nav>
            <!-- --- -->
        </svelte:fragment>
        <slot/>
    </AppShell>
	<SvelteQueryDevtools/>
</QueryClientProvider>
