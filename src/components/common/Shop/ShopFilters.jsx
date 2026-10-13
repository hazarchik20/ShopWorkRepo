import sortIcon from '@assets/arrow-switch-horizontal.svg'

function ShopFilters() {
  return (
    <div className="shop-filters">
      <div className="condition-buttons">
        <button type="button">Нові</button>
        <button type="button">Вживані</button>
      </div>

      <button className="sort-button" type="button">
        <img src={sortIcon} alt="" />
        <span>За популярністю</span>
      </button>
    </div>
  )
}

export default ShopFilters