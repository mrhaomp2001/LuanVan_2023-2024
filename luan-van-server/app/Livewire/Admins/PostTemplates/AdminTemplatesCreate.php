<?php

namespace App\Livewire\Admins\PostTemplates;

use App\Models\PostTemplate;
use Livewire\Component;

class AdminTemplatesCreate extends Component
{

    public $template;
    public $name;
    public $content;
    public $theme_color;
    public $is_require_title;
    public $is_require_image;
    public $can_use;

    public function mount()
    {
        $this->theme_color = "#ffffff";
        $this->is_require_title = false;
        $this->is_require_image = false;
        $this->can_use = false;
    }
    public function save()
    {
        $this->template = new PostTemplate();

        $this->template->name = $this->name;
        $this->template->content = $this->content;
        $this->template->theme_color = $this->theme_color;
        $this->template->is_require_title = $this->is_require_title;
        $this->template->is_require_image = $this->is_require_image;
        $this->template->can_use = $this->can_use;

        $this->template->save();

        $this->redirect(route("admin.template.index"));
    }
    public function render()
    {
        return view('livewire.admins.post-templates.admin-templates-create');
    }
}
