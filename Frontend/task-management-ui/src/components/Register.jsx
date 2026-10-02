import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { registerUser } from "../services/authService";
function Register() {

    const navigate = useNavigate();

    const [form, setForm] = useState({
        firstName: "",
        lastName: "",
        email: "",
        password: "",
        role: "User"
    });

    const [error, setError] = useState("");

    const handleChange = (e) => {

        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    };


    const handleRegister = async (e) => {

        e.preventDefault();

        setError("");

        try {

            await registerUser(form);

            alert("Registration successful");

            navigate("/login");

        } catch (error) {

            console.error(error);

            setError(
                error.response?.data?.message ||
                "Registration failed"
            );
        }
    };


    return (
        <div className="auth-container">

            <div className="auth-box">

                <h2>Create Account</h2>

                {error && (
                    <p className="error">
                        {error}
                    </p>
                )}

                <form onSubmit={handleRegister}>

                    <div className="form-group">

                        <label>First Name</label>

                        <input
                            name="firstName"
                            value={form.firstName}
                            onChange={handleChange}
                            required
                        />

                    </div>


                    <div className="form-group">

                        <label>Last Name</label>

                        <input
                            name="lastName"
                            value={form.lastName}
                            onChange={handleChange}
                            required
                        />

                    </div>


                    <div className="form-group">

                        <label>Email</label>

                        <input
                            type="email"
                            name="email"
                            value={form.email}
                            onChange={handleChange}
                            required
                        />

                    </div>


                    <div className="form-group">

                        <label>Password</label>

                        <input
                            type="password"
                            name="password"
                            value={form.password}
                            onChange={handleChange}
                            required
                        />

                    </div>


                    <div className="form-group">

                        <label>Role</label>

                        <select
                            name="role"
                            value={form.role}
                            onChange={handleChange}
                        >

                            <option value="User">
                                User
                            </option>

                            <option value="Manager">
                                Manager
                            </option>

                            <option value="Admin">
                                Admin
                            </option>

                        </select>

                    </div>


                    <button type="submit">
                        Register
                    </button>

                </form>


                <p>
                    Already have an account?
                    {" "}
                    <Link to="/login">
                        Login
                    </Link>
                </p>

            </div>

        </div>
    );
}

export default Register;