import ShopHero from '../../components/common/Shop/ShopHero'
import SearchBar from '../../components/common/SearchBar/SearchBar'
import ShopFilters from '../../components/common/Shop/ShopFilters'
import BookSection from '../../components/common/BookSection/BookSection'
import './Shop.scss'

function Shop() {
  return (
    <div className="shop-page">
      <div className="shop-top">
        <ShopHero />
        <div className="shop-search-position">
          <SearchBar />
        </div>
      </div>

      <main className="shop-content">
        <ShopFilters />
        <BookSection title="Пропозиції дня" />
      </main>
    </div>
  )
}

export default Shop