import react from '@vitejs/plugin-react'
import path from 'node:path'
import { defineConfig } from 'vite'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
        alias: {
            '@components': '/src/components',
            '@providers': '/src/providers',
            '@api': '/src/api',
            '@styles': '/src/styles',
            '@pages': '/src/pages',
            '@assets': path.resolve(import.meta.dirname, 'src/assets'),
            '@assets': '/src/assets',
        }
    }
})
