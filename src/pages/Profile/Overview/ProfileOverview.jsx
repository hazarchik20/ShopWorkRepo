
import { Link, useNavigate } from "react-router-dom";
import { ArrowUpRight, BookOpen } from "lucide-react";

import SupportBanner from "@components/common/SupportBanner/SupportBanner";

import avatar from "@assets/cat-avatar.png";

import "./ProfileOverview.scss";

const mockUser = {
    id: "1",
    firstName: "Олександр",
    lastName: "К.",
    email: "joikeh8@gmail.com",
    phoneNumber: null,
    dateOfBirth: "1998-03-29",
    booksRead: 0,
    avatar: avatar
};

const formatDate = (date) => {
    if (!date) return "Не вказано";

    const [year, month, day] = date.split("-");

    return `${day}.${month}.${year}`;
};

const ProfileOverview = () => {
    const navigate = useNavigate();

    const user = mockUser;

    const handleSupport = () => {
        navigate("/support-project");
    };

    return (
        <div className="profile-overview">

            <section className="profile-overview__info">

                <div className="profile-overview__avatar">
                    <img
                        src={user.avatar}
                        alt="Аватар користувача"
                    />
                </div>

                <div className="profile-overview__details">
                    <h2>
                        {[user.firstName, user.lastName]
                            .filter(Boolean)
                            .join(" ")}
                    </h2>

                    <p>
                        <span>Е-пошта:</span> {user.email}
                    </p>

                    <p>
                        <span>Телефон:</span>{" "}
                        {user.phoneNumber || "Не вказано"}
                    </p>
                </div>

                <div className="profile-overview__statistics">
                    <p className="profile-overview__books">
                        <BookOpen size={18} strokeWidth={1.8} />
                        <span>
                            Прочитано книг: {user.booksRead}
                        </span>
                    </p>

                    <p>
                        <span>Дата народження:</span>{" "}
                        {formatDate(user.dateOfBirth)}
                    </p>
                </div>

                <Link
                    to="/profile/edit"
                    className="profile-overview__edit"
                >
                    <span>Редагувати</span>

                    <span className="profile-overview__edit-icon">
                        <ArrowUpRight size={20} />
                    </span>
                </Link>

            </section>

            <div className="profile-overview__banner">
                <SupportBanner onSupportClick={handleSupport} />
            </div>

        </div>
    );
};

export default ProfileOverview;
