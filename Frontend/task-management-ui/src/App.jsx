import {
    BrowserRouter,
    Routes,
    Route,
    Link,
    useNavigate
} from "react-router-dom";

import Login
    from "./components/Login.jsx";

import Register
    from "./components/Register.jsx";

import Dashboard
    from "./components/Dashboard.jsx";

import Users
    from "./components/Users.jsx";

import Teams
    from "./components/Teams.jsx";
        
import Tasks
    from "./components/Tasks.jsx";

import Comments
    from "./components/Comments.jsx";

import ProtectedRoute
    from "./components/ProtectedRoute";

import {
    logoutUser
} from "./services/authService";


function Navigation() {

    const navigate = useNavigate();

    const token =
        localStorage.getItem("token");

    const role =
        localStorage.getItem("role");


    const handleLogout = () => {

        logoutUser();

        navigate("/login");
    };


    if (!token) {
        return null;
    }


    return (

        <nav className="navbar">

            <div>

                <Link to="/dashboard">
                    Dashboard
                </Link>

                <Link to="/tasks">
                    Tasks
                </Link>

                <Link to="/comments">
                    Comments
                </Link>

                <Link to="/teams">
                    Teams
                </Link>

                {role === "Admin" && (

                    <Link to="/users">
                        Users
                    </Link>

                )}

            </div>


            <div>

                <span>
                    Role: {role}
                </span>

                <button
                    onClick={handleLogout}
                >
                    Logout
                </button>

            </div>

        </nav>
    );
}


function App() {

    return (

        <BrowserRouter>

            <Navigation />

            <Routes>

                <Route
                    path="/"
                    element={
                        <Login />
                    }
                />


                <Route
                    path="/login"
                    element={
                        <Login />
                    }
                />


                <Route
                    path="/register"
                    element={
                        <Register />
                    }
                />


                <Route
                    path="/dashboard"
                    element={
                        <ProtectedRoute>
                            <Dashboard />
                        </ProtectedRoute>
                    }
                />


                <Route
                    path="/tasks"
                    element={
                        <ProtectedRoute>
                            <Tasks />
                        </ProtectedRoute>
                    }
                />


                <Route
                    path="/comments"
                    element={
                        <ProtectedRoute>
                            <Comments />
                        </ProtectedRoute>
                    }
                />


                <Route
                    path="/teams"
                    element={
                        <ProtectedRoute>
                            <Teams />
                        </ProtectedRoute>
                    }
                />


                <Route
                    path="/users"
                    element={
                        <ProtectedRoute>
                            <Users />
                        </ProtectedRoute>
                    }
                />

            </Routes>

        </BrowserRouter>
    );
}

export default App;