import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  ngOnInit(): void {
    this.test();
  }
  title = 'app';

  test(): void {
    fetch('/api/weatherforecast')
      .then(response => console.log(`Fetched from backend: `, response.json()))
    ;
  }
}
