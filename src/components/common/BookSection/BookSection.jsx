import bookPhoto from '@assets/book-photo.png'
import bookPlaceholder from '@assets/book-placeholder.png'
import arrowIcon from '@assets/arrow-left-contained-01.svg'
import BookCard from '@components/common/BookCard/BookCard'
import './BookSection.scss'

const defaultImages = [
  bookPhoto,
  bookPlaceholder,
  bookPhoto,
  bookPhoto,
]

function BookSection({ title, images = defaultImages }) {
  return (
    <section className="book-section">
      <div className="book-section-header">
        <h2>{title}</h2>

        <div className="book-section-arrows">
          <button type="button" aria-label="Попередні книги">
            <img src={arrowIcon} alt="" />
          </button>

          <button
            className="next-books"
            type="button"
            aria-label="Наступні книги"
          >
            <img src={arrowIcon} alt="" />
          </button>
        </div>
      </div>
      <div className="book-section-grid">
        {images.map((image, index) => (
          <BookCard
            key={index}
            title="Книга «Тією Горою є Ви»"
            price={240}
            image={image}
          />
        ))}
      </div>
    </section>
  )
}

export default BookSection