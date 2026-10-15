
import { useState, useRef, useEffect, useCallback } from "react";

import bookPhoto from "@assets/book-photo.png";
import bookPlaceholder from "@assets/book-placeholder.png";
import arrowIcon from "@assets/arrow-left-contained-01.svg";

import BookCard from "@components/common/BookCard/BookCard";

import "./BookSection.scss";

const defaultBooks = [
    { id: 1, title: "Книга «Тією Горою є Ви»", price: 240, image: bookPhoto },
    { id: 2, title: "Книга «1984»", price: 280, image: bookPlaceholder },
    { id: 3, title: "Книга «Атомні звички»", price: 320, image: bookPhoto },
    { id: 4, title: "Книга «Мистецтво війни»", price: 190, image: bookPhoto },
    { id: 5, title: "Книга «Психологія грошей»", price: 350, image: bookPlaceholder },
    { id: 6, title: "Книга «Кафе на краю світу»", price: 260, image: bookPhoto }
];

function BookSection({
    title,
    books,
    images,
    showArrows = true,
    variant = "default"
}) {
    const scrollRef = useRef(null);

    const [canScrollLeft, setCanScrollLeft] = useState(false);
    const [canScrollRight, setCanScrollRight] = useState(false);

    const items = books ?? (
        images
            ? images.map((image, index) => ({
                id: index,
                title: "Книга «Тією Горою є Ви»",
                price: 240,
                image
            }))
            : defaultBooks
    );

    const updateScrollButtons = useCallback(() => {
        const container = scrollRef.current;
        if (!container) return;

        const { scrollLeft, scrollWidth, clientWidth } = container;

        setCanScrollLeft(scrollLeft > 1);
        setCanScrollRight(
            scrollLeft + clientWidth < scrollWidth - 1
        );
    }, []);

    useEffect(() => {
        const container = scrollRef.current;
        if (!container) return;

        updateScrollButtons();

        const observer = new ResizeObserver(updateScrollButtons);
        observer.observe(container);

        return () => observer.disconnect();
    }, [items.length, updateScrollButtons]);

    const scroll = (direction) => {
        const container = scrollRef.current;
        if (!container) return;

        const firstCard = container.querySelector(".book-card");
        if (!firstCard) return;

        const styles = window.getComputedStyle(container);
        const gap = parseFloat(styles.columnGap) || 0;

        const cardWidth = firstCard.getBoundingClientRect().width;
        const scrollAmount = cardWidth + gap;

        container.scrollBy({
            left: direction * scrollAmount,
            behavior: "smooth"
        });
    };

    return (
        <section className="book-section">
            <div className="book-section-header">
                <h2>{title}</h2>

                {showArrows && (
                    <div className="book-section-arrows">
                        <button
                            type="button"
                            aria-label="Попередні книги"
                            disabled={!canScrollLeft}
                            onClick={() => scroll(-1)}
                        >
                            <img src={arrowIcon} alt="" />
                        </button>

                        <button
                            className="next-books"
                            type="button"
                            aria-label="Наступні книги"
                            disabled={!canScrollRight}
                            onClick={() => scroll(1)}
                        >
                            <img src={arrowIcon} alt="" />
                        </button>
                    </div>
                )}
            </div>

            <div
                ref={scrollRef}
                className="book-section-grid"
                onScroll={updateScrollButtons}
                aria-label={title || "Список книг"}
            >
                {items.map((book, index) => (
                    <BookCard
                        key={book.id ?? index}
                        {...book}
                        variant={variant}
                    />
                ))}
            </div>
        </section>
    );
}

export default BookSection;
