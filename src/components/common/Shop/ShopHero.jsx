import character from '@assets/shop-character.png'

function ShopHero() {
  return (
    <section className="shop-hero">
      <img
        className="shop-character"
        src={character}
        alt=""
      />

      <div className="shop-hero-text">
        <h1>Купуйте та продавайте книги безпечно</h1>
        <p>
          Платформа для продажу нових та вживаних книг з рук у руки.
        </p>
      </div>
    </section>
  )
}

export default ShopHero