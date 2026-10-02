import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { loginUser } from "../services/authService";

function Login() {

    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleLogin = async (e) => {

        e.preventDefault();

        setError("");

        try {

            // Call Login API
            const result = await loginUser({
                email: email,
                password: password
            });

            // Get JWT token
            const token = result.token;

            // Store JWT token
            localStorage.setItem("token", token);

            // Decode JWT payload
            const payload = JSON.parse(
                atob(token.split(".")[1])
            );

            // Get role from JWT
            const role =
                payload[
                    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                ] ||
                payload["role"];

            // Check role
            if (!role) {

                setError("User role not found in token.");

                return;
            }

            // Store role
            localStorage.setItem("role", role);

            console.log("Login successful");
            console.log("Role:", role);

            // Navigate to Dashboard
            navigate("/dashboard");

        } catch (error) {

            console.error(error);

            setError(
                error.response?.data?.message ||
                "Invalid email or password"
            );
        }
    };

    return (
        <div className="auth-container">

            <div className="auth-box">

                <h2>Team Task Management</h2>

                <h3>Login</h3>

                {error && (
                    <p className="error">
                        {error}
                    </p>
                )}

                <form onSubmit={handleLogin}>

                    {/* Email */}

                    <div className="form-group">

                        <label>Email</label>

                        <input
                            type="email"
                            value={email}
                            onChange={(e) =>
                                setEmail(e.target.value)
                            }
                            placeholder="Enter email"
                            required
                        />

                    </div>

                    {/* Password */}

                    <div className="form-group">

                        <label>Password</label>

                        <input
                            type="password"
                            value={password}
                            onChange={(e) =>
                                setPassword(e.target.value)
                            }
                            placeholder="Enter password"
                            required
                        />

                    </div>

                    {/* Login Button */}

                    <button type="submit">
                        Login
                    </button>

                </form>

                <p>
                    Don't have an account?{" "}
                    <Link to="/register">
                        Register
                    </Link>
                </p>

            </div>

        </div>
    );
}

export default Login;