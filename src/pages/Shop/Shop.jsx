import Header from '../../components/layout/Header/Header'
import ShopHero from '../../components/Shop/ShopHero'
import ShopSearch from '../../components/Shop/ShopSearch'
import ShopFilters from '../../components/Shop/ShopFilters'
import BookSection from '../../components/common/BookSection/BookSection'

function Shop() {
  return (
    <div className="shop-page">
      <div className="shop-top">
        <Header />
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