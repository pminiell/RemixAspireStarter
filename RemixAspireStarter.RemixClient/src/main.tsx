import { createRoot } from 'react-dom/client'
import { createBrowserRouter } from 'react-router'
import { RouterProvider } from 'react-router'
import { getWidgetsAsync } from './WidgetClient.ts'
import App from './App.tsx'

const router = createBrowserRouter([
  {
    path: "/",
    loader: getWidgetsAsync,
    Component: App
  }
])

createRoot(document.getElementById('root')!).render(
  <RouterProvider router={router} />
)
