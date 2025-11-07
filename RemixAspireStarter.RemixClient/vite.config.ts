import { defineConfig } from 'vite'
import { reactRouter } from '@react-router/dev/vite'
import tailwindcss from '@tailwindcss/vite'
// https://vite.dev/config/
export default defineConfig({
  plugins: [reactRouter(), tailwindcss()],
  server: {
    proxy: {
      '/api': {
        target: process.env['services__remixaspirestarter-api__http__o'],
        rewrite: (path) => path.replace(/^\/api/, ''),
        changeOrigin: true,
        secure: false,
      }
    }
  }
})


