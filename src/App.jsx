import Header from './components/Header'
import ShopHero from './components/ShopHero'
import ShopSearch from './components/ShopSearch'
import ShopFilters from './components/ShopFilters'
import BookSection from './components/BookSection'
import './style.css'

function App() {
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

export default App