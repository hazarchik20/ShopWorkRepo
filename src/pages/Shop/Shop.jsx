import Header from '../../components/layout/Header/Header'
import ShopHero from '../../components/common/Shop/ShopHero'
import ShopSearch from '../../components/common/Shop/ShopSearch'
import ShopFilters from '../../components/common/Shop/ShopFilters'
import BookSection from '../../components/common/BookSection/BookSection'

function Shop() {
  return (
    <div className="shop-page">
      <div className="shop-top">
        <ShopHero />
        <ShopSearch />
      </div>

      <main className="shop-content">
        <ShopFilters />
        <BookSection title="Пропозиції дня" />
      </main>
    </div>
  )
}

export default Shop