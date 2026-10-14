import bannerImage from '@assets/support-banner.png'
import './SupportBanner.scss'

function SupportBanner({ onSupportClick }) {
  return (
    <section
      className="support-banner"
      aria-label="Підтримка розвитку Readvia"
    >
      <img
        className="support-banner-background"
        src={bannerImage}
        alt=""
      />

      <h2 className="support-banner-title">
        Підтримай розвиток Readvia
      </h2>

      <p className="support-banner-description">
        Ваша підтримка допомагає нам покращувати сервіс
        та боротися за культуру читання.
      </p>

      <button
        className="support-banner-button"
        type="button"
        onClick={onSupportClick}
      >
        Підтримати проект
      </button>
    </section>
  )
}

export default SupportBanner