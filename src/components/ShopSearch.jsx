import searchIcon from '../assets/search.svg'
import catalogIcon from '../assets/catalog.svg'
import markerIcon from '../assets/marker-02.svg'

function ShopSearch() {
    return (
        <div className="shop-search-controls">
            <button className="location-button" type="button">
                <img src={markerIcon} alt="" />
                <span>Уся Україна</span>
            </button>

            <div className="shop-search">
                <img
                    className="search-icon"
                    src={searchIcon}
                    alt=""
                />

                <input
                    type="search"
                    aria-label="Пошук книг"
                    placeholder="Хто шукає той знаходить те що треба"
                />

                <button className="catalog-button" type="button">
                    <img src={catalogIcon} alt="" />
                    <span>Каталог</span>
                </button>
            </div>

            <button className="add-listing-button" type="button">
                Додати оголошення
            </button>
        </div>
    )
}

export default ShopSearch