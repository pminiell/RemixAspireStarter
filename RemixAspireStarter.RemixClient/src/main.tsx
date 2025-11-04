import { createRoot } from 'react-dom/client'
import { createBrowserRouter } from 'react-router'
import { RouterProvider } from 'react-router'
import { getWidgetsAsync } from './WidgetClient.ts'
import WidgetList from './Components/WidgetList.tsx'
import MainLayout from './Layouts/MainLayout.tsx'
import Counter from './Components/Counter.tsx'
import Home from './Components/Home.tsx'

const router = createBrowserRouter([
  {
    Component: MainLayout,
    children: [
      {
        index: true,
        Component: Home,
      },
      {
        path: 'widgets',
        Component: WidgetList,
        loader: getWidgetsAsync,
      },
      {
        path: 'counter',
        Component: Counter
      }
    ],
  }
])

createRoot(document.getElementById('root')!).render(
  <RouterProvider router={router} />
)
