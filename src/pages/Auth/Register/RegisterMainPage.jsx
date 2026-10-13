
import { useState } from "react";
import { Link } from "react-router-dom";
import { ArrowLeft, Eye, EyeOff } from "lucide-react";

import "../Auth.scss";

import background from "@assets/login-register-background.png";
import logo from "@assets/logo.png";

const Register = () => {
    const [form, setForm] = useState({
        name: "",
        password: "",
        confirmPassword: "",
        acceptedTerms: false
    });

    const [errors, setErrors] = useState({});
    const [showPassword, setShowPassword] = useState(false);
    const [showConfirmPassword, setShowConfirmPassword] = useState(false);

    const validate = (values) => {
        const validationErrors = {};

        if (!values.name.trim()) {
            validationErrors.name = "Введіть ім'я";
        } else if (!/^[\p{L}\s'-]+$/u.test(values.name.trim())) {
            validationErrors.name = "Ім'я не може містити цифри або символи";
        }

        if (!values.password) {
            validationErrors.password = "Введіть пароль";
        } else if (values.password.length < 8) {
            validationErrors.password = "Пароль повинен містити мінімум 8 символів";
        }

        if (!values.confirmPassword) {
            validationErrors.confirmPassword = "Повторіть пароль";
        } else if (values.password !== values.confirmPassword) {
            validationErrors.confirmPassword = "Паролі не збігаються";

            if (!validationErrors.password) {
                validationErrors.password = "Паролі не збігаються";
            }
        }

        if (!values.acceptedTerms) {
            validationErrors.acceptedTerms = "Потрібно погодитися з правилами";
        }

        return validationErrors;
    };

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;

        setForm((prev) => ({
            ...prev,
            [name]: type === "checkbox" ? checked : value
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        const validationErrors = validate(form);
        setErrors(validationErrors);

        if (Object.keys(validationErrors).length > 0) {
            return;
        }

        // TODO: authService.register(form)
        console.log("Форма валідна, готова до підключення API");
    };

    return (
        <div className="auth-page">
            <div className="auth-container">
                <div className="auth-panel">

                    <Link to="/login" className="auth-back">
                        <ArrowLeft size={20} />
                    </Link>

                    <div className="auth-content auth-register-content">
                        <img
                            src={logo}
                            alt="Readvia"
                            className="auth-logo"
                        />

                        <form onSubmit={handleSubmit} noValidate>
                            <div className="auth-field">
                                <label className="auth-label">
                                    Ім'я
                                </label>

                                <input
                                    type="text"
                                    name="name"
                                    placeholder="Як до вас звертатися?"
                                    className={`auth-input ${errors.name ? "auth-input-error" : ""}`}
                                    value={form.name}
                                    onChange={handleChange}
                                />

                                {errors.name && (
                                    <p className="auth-error">
                                        {errors.name}
                                    </p>
                                )}
                            </div>

                            <div className="auth-field">
                                <label className="auth-label">
                                    Створіть пароль
                                </label>

                                <div className={`auth-password ${errors.password ? "auth-input-error" : ""}`}>
                                    <input
                                        type={showPassword ? "text" : "password"}
                                        name="password"
                                        placeholder="Password"
                                        value={form.password}
                                        onChange={handleChange}
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

                            <div className="auth-field">
                                <label className="auth-label">
                                    Повторіть пароль
                                </label>

                                <div className={`auth-password ${errors.confirmPassword ? "auth-input-error" : ""}`}>
                                    <input
                                        type={showConfirmPassword ? "text" : "password"}
                                        name="confirmPassword"
                                        placeholder="Password"
                                        value={form.confirmPassword}
                                        onChange={handleChange}
                                    />

                                    <button
                                        type="button"
                                        onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                                    >
                                        {showConfirmPassword
                                            ? <EyeOff size={18} />
                                            : <Eye size={18} />}
                                    </button>
                                </div>

                                {errors.confirmPassword && (
                                    <p className="auth-error">
                                        {errors.confirmPassword}
                                    </p>
                                )}
                            </div>

                            <label className={`auth-checkbox ${errors.acceptedTerms ? "auth-checkbox-error" : ""}`}>
                                <input
                                    type="checkbox"
                                    name="acceptedTerms"
                                    checked={form.acceptedTerms}
                                    onChange={handleChange}
                                />

                                <span>
                                    Я погоджуюся з Правилами спільноти
                                </span>
                            </label>

                            <button
                                type="submit"
                                className="auth-submit"
                            >
                                Завершити реєстрацію
                            </button>
                        </form>
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

export default Register;
