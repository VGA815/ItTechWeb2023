import logo from './logo.svg';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <form>
      <p>
        <label>Имя:</label><br />
        <input type="text" />
      </p>
      <input type="submit" value="Отправить" />
    </form>
  );
}

export default App;
