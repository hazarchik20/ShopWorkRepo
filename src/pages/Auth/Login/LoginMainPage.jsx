
import { useState } from "react";
import { Link } from "react-router-dom";
import { Eye, EyeOff, ArrowLeft } from "lucide-react";

import "../Auth.scss";

import background from "@assets/login-register-background.png";
import logo from "@assets/logo-black.png";
import google_image from "@assets/google-icon.png";

const LoginMainPage = () => {
    const [step, setStep] = useState(1);

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [showPassword, setShowPassword] = useState(false);
    const [errors, setErrors] = useState({});

    const validateEmail = () => {
        if (!email.trim()) {
            return "Введіть електронну пошту";
        }

        if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email.trim())) {
            return "Некоректна електронна пошта";
        }

        return null;
    };

    const validatePassword = () => {
        if (!password) {
            return "Введіть пароль";
        }

        return null;
    };

    const handleContinue = (e) => {
        e.preventDefault();

        const emailError = validateEmail();

        if (emailError) {
            setErrors({ email: emailError });
            return;
        }

        setErrors({});
        setStep(2);
    };

    const handleLogin = (e) => {
        e.preventDefault();

        const passwordError = validatePassword();

        if (passwordError) {
            setErrors({ password: passwordError });
            return;
        }

        setErrors({});

        // TODO: authService.login({ email, password })
        console.log("Форма входу валідна");
    };

    const handleBack = () => {
        setStep(1);
        setErrors({});
    };

    return (
        <div className="auth-page">
            <div className="auth-container">
                <div className="auth-panel">

                    {step === 2 && (
                        <button
                            type="button"
                            className="auth-back"
                            onClick={handleBack}
                        >
                            <ArrowLeft size={20} />
                        </button>
                    )}

                    <div className="auth-content">
                        <img
                            src={logo}
                            alt="Readvia"
                            className="auth-logo"
                        />


                        {step === 1 ?
                            (
                                <>

                                    <p className="auth-subtitle">
                                        Увійти / Зареєструватися через
                                    </p>
                                    <button
                                        type="button"
                                        className="auth-google"
                                        onClick={() => {
                                            // TODO: Google OAuth
                                        }}
                                    >
                                        <img
                                            src={google_image}
                                            alt="Google"
                                            className="google-icon"
                                        />
                                        Google
                                    </button>

                                    <div className="auth-divider">
                                        <span>або</span>
                                    </div>

                                    <form onSubmit={handleContinue} noValidate>
                                        <div className="auth-field">
                                            <input
                                                type="email"
                                                placeholder="Email"
                                                className={`auth-input ${errors.email ? "auth-input-error" : ""}`}
                                                value={email}
                                                onChange={(e) => setEmail(e.target.value)}
                                            />

                                            {errors.email && (
                                                <p className="auth-error">
                                                    {errors.email}
                                                </p>
                                            )}
                                        </div>

                                        <button
                                            type="submit"
                                            className="auth-submit"
                                        >
                                            Продовжити
                                        </button>
                                    </form>

                                    <p className="auth-footer-text">
                                        Ще не маєте профілю?{" "}
                                        <Link to="/register">
                                            Створити акаунт
                                        </Link>
                                    </p>
                                </>
                            ) : (
                                <form onSubmit={handleLogin} noValidate>
                                    <p className="auth-subtitle">
                                        Введть пароль, щоб увійти в акунт 
                                    </p>
                                    <p className="auth-email">
                                        {email}
                                    </p>

                                    <div className="auth-field">
                                        <label className="auth-label">
                                            Пароль
                                        </label>

                                        <div className={`auth-password ${errors.password ? "auth-input-error" : ""}`}>
                                            <input
                                                type={showPassword ? "text" : "password"}
                                                placeholder="Password"
                                                value={password}
                                                onChange={(e) => setPassword(e.target.value)}
                                            />

                                            <button
                                                type="button"
                                                onClick={() => setShowPassword(!showPassword)}
                                            >
                                                {showPassword
                                                    ? <EyeOff size={18} />
                                                    : <Eye size={18} />}
                                            </button>
                                        </div>

                                        {errors.password && (
                                            <p className="auth-error">
                                                {errors.password}
                                            </p>
                                        )}
                                    </div>

                                    <button
                                        type="submit"
                                        className="auth-submit"
                                    >
                                        Увійти
                                    </button>
                                </form>
                            )}
                    </div>
                </div>

                <div className="auth-image">
                    <img
                        src={background}
                        alt="Readvia background"
                    />
                </div>
            </div>
        </div>
    );
};

export default LoginMainPage;
