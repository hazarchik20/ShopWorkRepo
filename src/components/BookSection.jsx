import arrowIcon from '../assets/arrow-left-contained-01.svg'

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
    </section>
  )
}

export default BookSection