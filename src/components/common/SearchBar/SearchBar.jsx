
import searchIcon from "@assets/search.svg";
import catalogIcon from "@assets/catalog.svg";
import markerIcon from "@assets/marker-02.svg";

import "./SearchBar.scss";

function SearchBar({ variant = "default", className = "" }) {
    return (
        <div className={`search-bar search-bar--${variant} ${className}`} role="search">
            <button
                className="search-bar__location"
                type="button"
            >
                <img src={markerIcon} alt="" />
                <span>Уся Україна</span>
            </button>

            <div className="search-bar__search">
                <img
                    className="search-bar__icon"
                    src={searchIcon}
                    alt=""
                />

                <input
                    type="search"
                    aria-label="Пошук книг"
                    placeholder="Хто шукає той знаходить те що треба"
                />

                <button
                    className="search-bar__catalog"
                    type="button"
                >
                    <img src={catalogIcon} alt="" />
                    <span>Каталог</span>
                </button>
            </div>

            <button
                className="search-bar__add"
                type="button"
            >
                Додати оголошення
            </button>
        </div>
    );
}

export default SearchBar;
