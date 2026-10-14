import bookHeart from '@assets/book-heart.svg'
import './BookCard.scss'

function BookCard({ title, price, image }) {
    return (
        <article className="book-card">
            <img
                className="book-card-image"
                src={image}
                alt={title}
            />

            <h3 className="book-card-title">
                {title}
            </h3>

            <span className="book-card-condition">
                Вживане
            </span>

            <div className="book-card-details">
                <p className="book-card-location">
                    Вінниця - 09 січня 2026 р.
                </p>

                <p className="book-card-price">
                    {price} грн.
                </p>
                <button
                    className="book-card-favorite"
                    type="button"
                    aria-label="Додати книгу в обране"
                >
                    <img src={bookHeart} alt="" />
                </button>
            </div>
        </article>
    )
}

export default BookCard