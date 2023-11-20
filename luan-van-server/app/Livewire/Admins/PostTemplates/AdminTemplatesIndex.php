<?php

namespace App\Livewire\Admins\PostTemplates;

use App\Models\PostTemplate;
use Livewire\Component;
use Livewire\WithPagination;

class AdminTemplatesIndex extends Component
{
    use WithPagination;

    public function render()
    {
        return view('livewire.admins.post-templates.admin-templates-index', 
        [
            "post_templates" => PostTemplate::paginate(10),
        ]);
    }
}
