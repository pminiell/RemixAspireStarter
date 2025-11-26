import { NavLink, Outlet } from "react-router";

function NavLayout() {
  return (

    <div className="min-h-screen flex flex-col bg-gray-100">
      {/* Header */}
      <header className="bg-blue-600 text-white p-4">
        <nav className="container mx-auto flex justify-between items-center">
          <h1 className="text-2xl font-bold">My App</h1>
          <ul className="flex space-x-4">
            <li>
              <NavLink to="/" className="hover:underline">
                Home
              </NavLink>
            </li>
            <li>
              <NavLink to="/widgets" className="hover:underline"> Widgets
              </NavLink>
            </li>
            <li>
              <NavLink to="/counter" className="hover:underline">
                Counter
              </NavLink>
            </li>
          </ul>
        </nav>
      </header>

      {/* Main Content */}
      <main className="flex-grow container mx-auto p-4">
        <Outlet />
      </main>

      {/* Footer */}
      <footer className="bg-gray-800 text-white text-center p-4">
        <p>&copy; 2025 Remix Aspire Starter</p>
      </footer>
    </div>)
}

export default NavLayout;
