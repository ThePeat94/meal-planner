<script lang="ts">
    import { browser } from '$app/environment'
    import { QueryClient, QueryClientProvider } from '@tanstack/svelte-query';
    import { SvelteQueryDevtools } from '@tanstack/svelte-query-devtools';
    import axios from 'axios';

    const queryClient = new QueryClient({
        defaultOptions: {
            queries: {
                enabled: browser,
            },
        },
    })
  
    axios.defaults.baseURL = 'localhost:5190';
    axios.defaults.headers.post['Content-Type'] = 'application/json';
    axios.defaults.headers.post['Accept'] = 'application/json';
    axios.defaults.headers.get['Accept'] = 'application/json';
    axios.defaults.headers.get['Content-Type'] = 'application/json';
</script>


<QueryClientProvider client="{queryClient}">
    <slot/>
    <SvelteQueryDevtools/>
</QueryClientProvider>