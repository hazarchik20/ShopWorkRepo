import logo from '../assets/logo.png'
import cart from '../assets/cart.svg'
import heart from '../assets/heart.svg'
import user from '../assets/user-profile-01.svg'

function Header() {
    return (
        <header className="shop-header">
            <a className="logo" href="/">
                <img src={logo} alt="Readvia" />
            </a>

            <nav className="header-menu" aria-label="Основна навігація">
                <button type="button">Приєднатися до клубу</button>
                <button type="button" className="active" aria-current="page">
                    Readvia Market
                </button>
                <button type="button">Блог</button>
                <button type="button">Підписки</button>
            </nav>

            <div className="header-actions">
                <button type="button" aria-label="Кошик">
                    <img src={cart} alt="" />
                </button>

                <button type="button" aria-label="Обране">
                    <img src={heart} alt="" />
                </button>

                <button type="button" aria-label="Профіль">
                    <img src={user} alt="" />
                </button>
            </div>
        </header>
    )
}

export default Header