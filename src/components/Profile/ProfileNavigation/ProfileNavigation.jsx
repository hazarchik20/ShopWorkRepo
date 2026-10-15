
import { NavLink, useNavigate } from "react-router-dom";

import {
    UserRound,
    BookOpen,
    ClipboardList,
    Heart,
    Send,
    LogOut
} from "lucide-react";

import "./ProfileNavigation.scss";

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

const ProfileNavigation = () => {
    const navigate = useNavigate();

    const handleLogout = () => {
        // TODO: authService.logout()
        navigate("/login");
    };

    return (
        <nav
            className="profile-navigation"
            aria-label="Навігація особистого кабінету"
        >
            {menuItems.map(({ title, path, icon: Icon }) => (
                <NavLink
                    key={path}
                    to={path}
                    end={path === "/profile"}
                    className={({ isActive }) =>
                        `profile-nav-item ${isActive ? "active" : ""}`
                    }
                >
                    <Icon size={18} strokeWidth={1.8} />
                    <span>{title}</span>
                </NavLink>
            ))}

            <button
                type="button"
                className="profile-nav-item profile-logout"
                onClick={handleLogout}
            >
                <LogOut size={18} strokeWidth={1.8} />
                <span>Вихід</span>
            </button>
        </nav>
    );
};

export default ProfileNavigation;
