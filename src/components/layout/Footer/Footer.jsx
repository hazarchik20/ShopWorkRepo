
import { Link } from "react-router-dom";

import logo from "@assets/logo-white.png";
import tiktok from "@assets/tiktok.png";
import instagram from "@assets/instagram.png";
import telegram from "@assets/telegram.png";
import "./Footer.scss";

const Footer = () => {
    return (
        <footer className="footer">
            <div className="footer-container">
                <div className="footer-top">
                    <div className="footer-brand">
                        <Link to="/">
                            <img src={logo} alt="Readvia" />
                        </Link>
                    </div>

                    <div className="footer-column">
                        <h4>Клуби</h4>

                        <Link to="/clubs/join">
                            Приєднатися до клубу
                        </Link>

                        <Link to="/clubs/create">
                            Створити книжковий клуб
                        </Link>
                    </div>

                    <div className="footer-column">
                        <h4>Дослідити</h4>

                        <Link to="/books">
                            Знайти книгу
                        </Link>

                        <Link to="/shop">
                            Наш магазин
                        </Link>
                    </div>

                    <div className="footer-column">
                        <h4>Дізнатися більше</h4>

                        <Link to="/about">
                            Про нас
                        </Link>

                        <Link to="/blog">
                            Блог
                        </Link>
                    </div>

                    <div className="footer-column footer-social">
                        <h4>Слідкуйте за нами</h4>

                        <div className="footer-social-icons">
                            <a
                                href="https://instagram.com"
                                target="_blank"
                                rel="noopener noreferrer"
                                aria-label="Instagram"
                            >
                               <img src={instagram} alt="instagram" />

                            </a>

                            <a
                                href="https://tiktok.com"
                                target="_blank"
                                rel="noopener noreferrer"
                                aria-label="TikTok"
                            >
                                <img src={tiktok} alt="tiktok" />
                            </a>

                            <a
                                href="https://telegram.org"
                                target="_blank"
                                rel="noopener noreferrer"
                                aria-label="Telegram"
                            >
                                <img src={telegram} alt="telegram" />
                            </a>
                        </div>
                    </div>
                </div>

                <div className="footer-bottom">
                    <div className="footer-copyright">
                        © Readvia 2026. All rights reserved.
                    </div>

                    <div className="footer-legal">
                        <Link to="/privacy">
                            Конфіденційність
                        </Link>

                        <Link to="/copyright">
                            Політика авторського права
                        </Link>

                        <Link to="/terms">
                            Умови надання послуг
                        </Link>

                        <Link to="/support">
                            Служба підтримки
                        </Link>
                    </div>

                    <Link to="/donate" className="footer-donate">
                        Підтримати проєкт
                    </Link>
                </div>
            </div>
        </footer>
    );
};

export default Footer;
