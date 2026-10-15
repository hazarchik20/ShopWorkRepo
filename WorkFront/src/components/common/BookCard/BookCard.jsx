
import bookHeart from "@assets/book-heart.svg";
import "./BookCard.scss";

function BookCard({
    title,
    price,
    image,
    condition = "Вживане",
    location,
    date,
    variant = "default",
    onFavorite
}) {
    return (
        <article className={`book-card book-card--${variant}`}>
            <img
                className="book-card-image"
                src={image}
                alt={title}
            />

            <h3 className="book-card-title">
                {title}
            </h3>

            <span className="book-card-condition">
                {condition}
            </span>

            <div className="book-card-details">
                <p className="book-card-location">
                    {[location, date].filter(Boolean).join(" - ")}
                </p>

                <p className="book-card-price">
                    {price} грн.
                </p>

                {variant !== "listing" && (
                    <button
                        className="book-card-favorite"
                        type="button"
                        aria-label="Додати книгу в обране"
                        onClick={onFavorite}
                    >
                        <img src={bookHeart} alt="" />
                    </button>
                )}
            </div>
        </article>
    );
}

export default BookCard;
