
import { NavLink, Link, useNavigate } from "react-router-dom";
import {
    UserRound,
    BookOpen,
    ClipboardList,
    Heart,
    Send,
    LogOut,
    ArrowUpRight
} from "lucide-react";

import SupportBanner from "@components/common/SupportBanner/SupportBanner";
import avatar from "@assets/cat-avatar.png";

import "./Profile.scss";

const Profile = () => {
    const navigate = useNavigate();

    const user = {
        name: "Олександр К.",
        email: "joikeh8@gmail.com",
        phone: "",
        dateOfBirth: "29.03.1998",
        booksRead: 0
    };

    const menuItems = [
        {
            title: "Особистий кабінет",
            path: "/profile",
            icon: UserRound
        },
        {
            title: "Мої замовлення",
            path: "/profile/orders",
            icon: BookOpen
        },
        {
            title: "Мої оголошення",
            path: "/profile/listings",
            icon: ClipboardList
        },
        {
            title: "Список бажань",
            path: "/profile/wishlist",
            icon: Heart
        },
        {
            title: "Служба підтримки",
            path: "/profile/support",
            icon: Send
        }
    ];

    const handleLogout = () => {
        // TODO: authService.logout()
        navigate("/login");
    };

    const handleSupport = () => {
        navigate("/support-project");
    };

    return (
        <div className="profile-page">
            <div className="profile-container">

                <nav className="profile-navigation">
                    {menuItems.map((item) => {
                        const Icon = item.icon;

                        return (
                            <NavLink
                                key={item.path}
                                to={item.path}
                                end={item.path === "/profile"}
                                className={({ isActive }) =>
                                    `profile-nav-item ${isActive ? "active" : ""}`
                                }
                            >
                                <Icon size={18} />
                                <span>{item.title}</span>
                            </NavLink>
                        );
                    })}

                    <button
                        type="button"
                        className="profile-nav-item profile-logout"
                        onClick={handleLogout}
                    >
                        <LogOut size={18} />
                        <span>Вихід</span>
                    </button>
                </nav>

                <section className="profile-info">
                    <div className="profile-avatar">
                        <img src={avatar} alt="Аватар користувача" />
                    </div>

                    <div className="profile-details">
                        <h2>{user.name}</h2>

                        <p>
                            Е-пошта: {user.email}
                        </p>

                        <p>
                            Телефон: {user.phone || "—"}
                        </p>
                    </div>

                    <div className="profile-statistics">
                        <p>
                            <BookOpen size={17} />
                            <span>Прочитано книг: {user.booksRead}</span>
                        </p>

                        <p>
                            Дата народження: {user.dateOfBirth}
                        </p>
                    </div>

                    <Link
                        to="/profile/edit"
                        className="profile-edit"
                    >
                        <span>Редагувати</span>

                        <span className="profile-edit-icon">
                            <ArrowUpRight size={20} />
                        </span>
                    </Link>
                </section>

                <SupportBanner onSupportClick={handleSupport} />

            </div>
        </div>
    );
};

export default Profile;
