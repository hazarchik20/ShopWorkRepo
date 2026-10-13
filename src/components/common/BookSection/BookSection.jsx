import arrowIcon from '../../../assets/arrow-left-contained-01.svg'
import BookCard from '../BookCard/BookCard'

function BookSection({ title }) {
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
        <BookCard title="Книга «Тією Горою є Ви»" price={240} />
        <BookCard title="Книга «Тією Горою є Ви»" price={300} />
        <BookCard title="Книга «Тією Горою є Ви»" price={240} />
        <BookCard title="Книга «Тією Горою є Ви»" price={240} />
      </div>
    </section>
  )
}

export default BookSection